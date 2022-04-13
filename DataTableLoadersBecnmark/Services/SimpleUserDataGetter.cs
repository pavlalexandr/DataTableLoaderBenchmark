using MinimalKDemo.Domain;

namespace MinimalKDemo.Services
{
    public class SimpleUserDataGetter : IUserDataGetter
    {
        public UserDataClass[] GetClassData(IEnumerable<int> userIds)
        {
            var result = new List<UserDataClass>();
            foreach (var userId in userIds)
                result.Add(new UserDataClass()
                {
                    UserId = userId,
                    Value = (decimal)Random.Shared.NextDouble()
                });
            return result.ToArray();
        }

        public Task<UserDataClass[]> GetClassDataAsync(IEnumerable<int> userIds)
        {
            return Task.FromResult(GetClassData(userIds));
        }

        public UserDataStruct[] GetData(IEnumerable<int> userIds)
        {
            
            var result = new UserDataStruct[userIds.Count()];
            for (int i = 0; i < userIds.Count(); i++)
                result[i] = new UserDataStruct()
                {
                    UserId = userIds.ElementAt(i),
                    Value = (decimal)Random.Shared.NextDouble()
                };
            return result;
        }

        public Task<UserDataStruct[]> GetDataAsync(IEnumerable<int> userIds)
        {
            return Task.FromResult(GetData(userIds));
        }
    }
}
