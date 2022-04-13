using System.Collections.Concurrent;
using System.Data;

namespace MinimalKDemo.Services
{
    public class DataTableLoader
    {
        private const string UserIdColumnName = "user_id";
        private const string UserValueColumnName = "user_value";
        private readonly object _dataTableRowsWriteMutex = new();
        
        public async Task FillClassDataAsync(DataTable dataTable, IUserDataGetter userDataGetter)
        {
            ConcurrentDictionary<int, decimal> data = new();

            var chankSize = dataTable.Rows.Count / Environment.ProcessorCount;
            var usersIds = dataTable.Rows.Cast<DataRow>().Select(o => (int)o[UserIdColumnName]).ToArray().Chunk(chankSize);

            await Parallel.ForEachAsync(usersIds, (userIds, ct) => FillClassChankDataAsync(data, userDataGetter, userIds));

            foreach (DataRow row in dataTable.Rows.Cast<DataRow>().Where(o => data.ContainsKey((int)o[UserIdColumnName])))
                row[UserValueColumnName] = data[(int)row[UserIdColumnName]];
        }

        public async Task FillDataAsync(DataTable dataTable, IUserDataGetter userDataGetter)
        {
            ConcurrentDictionary<int, decimal> data = new();

            var chankSize = dataTable.Rows.Count / Environment.ProcessorCount;
            var usersIds = dataTable.Rows.Cast<DataRow>().Select(o => o.Field<int>(UserIdColumnName)).ToArray().Chunk(chankSize);

            await Parallel.ForEachAsync(usersIds, (userIds, ct) => FillChankDataAsync(data, userDataGetter, userIds));

            foreach (DataRow row in dataTable.Rows.Cast<DataRow>().Where(o => data.ContainsKey((int)o[UserIdColumnName])))
                row[UserValueColumnName] = data[(int)row[UserIdColumnName]];
        }

        public void FillData(DataTable dataTable, IUserDataGetter userDataGetter)
        {
            ConcurrentDictionary<int, decimal> data = new ConcurrentDictionary<int, decimal>();

            var chankSize = dataTable.Rows.Count / Environment.ProcessorCount;
            var usersIds = dataTable.Rows.Cast<DataRow>().Select(o => o.Field<int>(UserIdColumnName)).ToArray().Chunk(chankSize);

            Parallel.ForEach(usersIds, (userIds) => FillChankData(data, userDataGetter, userIds));

            foreach (DataRow row in dataTable.Rows.Cast<DataRow>().Where(o => data.ContainsKey(o.Field<int>(UserIdColumnName))))
                row[UserValueColumnName] = data[row.Field<int>(UserIdColumnName)];

        }

        public void FillTableWithoutConcurrency(DataTable dataTable, IUserDataGetter userDataGetter)
        {
            var usersIds = dataTable.Rows.Cast<DataRow>().Select(o => o.Field<int>(UserIdColumnName)).ToArray();
            var usersDataDictionary = userDataGetter.GetData(usersIds).ToDictionary(o => o.UserId, o => o.Value);
            foreach (var row in dataTable.Rows.Cast<DataRow>().Where(o => usersDataDictionary.ContainsKey(o.Field<int>(UserIdColumnName))))
            {
                row[UserValueColumnName] = usersDataDictionary[row.Field<int>(UserIdColumnName)];
            }
        }


        public void FillDataTable(DataTable dataTable, IUserDataGetter userDataGetter)
        {
            var chankSize = dataTable.Rows.Count / Environment.ProcessorCount;

            var usersIds = dataTable.Rows.Cast<DataRow>().Select(o => o.Field<int>(UserIdColumnName)).ToArray().Chunk(chankSize);
            object syncObject = new object();
            Parallel.ForEach(usersIds, (userIds) => FillChankData(dataTable, userDataGetter, userIds));
        }

        private void FillChankData(ConcurrentDictionary<int, decimal> datatable, IUserDataGetter userDataGetter, int[] userIds)
        {

            var usersData = userDataGetter.GetData(userIds);

            foreach (var userData in usersData)
                datatable.TryAdd(userData.UserId, userData.Value);
        }

        private async ValueTask FillChankDataAsync(ConcurrentDictionary<int, decimal> datatable, IUserDataGetter userDataGetter, IEnumerable<int> userIds)
        {

            var usersData = await userDataGetter.GetDataAsync(userIds);

            foreach (var userData in usersData)
                datatable.TryAdd(userData.UserId, userData.Value);
        }

        private async ValueTask FillClassChankDataAsync(ConcurrentDictionary<int, decimal> datatable, IUserDataGetter userDataGetter, int[] userIds)
        {

            var usersData = await userDataGetter.GetClassDataAsync(userIds);

            foreach (var userData in usersData)
                datatable.TryAdd(userData.UserId, userData.Value);
        }
        
        private void FillChankData(DataTable datatable, IUserDataGetter userDataGetter, int[] userIds)
        {
            var usersDataDictionary = userDataGetter.GetData(userIds).ToDictionary(o => o.UserId, o => o.Value);


            foreach (var row in datatable.Rows.Cast<DataRow>().Where(o => usersDataDictionary.ContainsKey(o.Field<int>(UserIdColumnName))))
            {
                lock (_dataTableRowsWriteMutex)
                    row[UserValueColumnName] = usersDataDictionary[row.Field<int>(UserIdColumnName)];
            }
        }
    }
}
