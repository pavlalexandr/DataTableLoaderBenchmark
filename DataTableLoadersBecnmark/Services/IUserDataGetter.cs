using MinimalKDemo.Domain;

namespace MinimalKDemo.Services
{
    public interface IUserDataGetter
    {
        UserDataStruct[] GetData(IEnumerable<int> userIds);
        Task<UserDataStruct[]> GetDataAsync(IEnumerable<int> userIds);
        UserDataClass[] GetClassData(IEnumerable<int> userIds);
        Task<UserDataClass[]> GetClassDataAsync(IEnumerable<int> userIds);
    }
}
