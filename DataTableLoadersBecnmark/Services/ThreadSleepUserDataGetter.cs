using MinimalKDemo.Domain;

namespace MinimalKDemo.Services
{
    public class ThreadSleepUserDataGetter : IUserDataGetter
    {
        public UserDataClass[] GetClassData(IEnumerable<int> userIds)
        {
            var iserIdsCount = userIds.Count();
            var result = new UserDataClass[iserIdsCount];

            for (int i = 0; i < iserIdsCount; i++)
                result[i] = new UserDataClass()
                {
                    UserId = userIds.ElementAt(i),
                    Value = (decimal)Random.Shared.NextDouble()
                };
            Thread.Sleep(iserIdsCount / 1000);

            return result;
        }

        public async Task<UserDataClass[]> GetClassDataAsync(IEnumerable<int> userIds)
        {
            var iserIdsCount = userIds.Count();
            await Task.Delay(iserIdsCount / 1000);

            var result = new UserDataClass[iserIdsCount];

            for (int i = 0; i < iserIdsCount; i++)
                result[i] = new UserDataClass()
                {
                    UserId = userIds.ElementAt(i),
                    Value = (decimal)Random.Shared.NextDouble()
                };
            return result;
        }

        public UserDataStruct[] GetData(IEnumerable<int> userIds)
        {
            var iserIdsCount = userIds.Count();
            var result = new UserDataStruct[iserIdsCount];
            for (int i = 0; i < iserIdsCount; i++)
                result[i] = new UserDataStruct()
                {
                    UserId = userIds.ElementAt(i),
                    Value = (decimal)Random.Shared.NextDouble()
                };
            Thread.Sleep(iserIdsCount / 1000);
            return result;
        }

        public async Task<UserDataStruct[]> GetDataAsync(IEnumerable<int> userIds)
        {
            var iserIdsCount = userIds.Count();
            await Task.Delay(iserIdsCount / 1000);
            var result = new UserDataStruct[iserIdsCount];
            for (int i = 0; i < iserIdsCount; i++)
                result[i] = new UserDataStruct()
                {
                    UserId = userIds.ElementAt(i),
                    Value = (decimal)Random.Shared.NextDouble()
                };
            return result;
        }
    }
}
