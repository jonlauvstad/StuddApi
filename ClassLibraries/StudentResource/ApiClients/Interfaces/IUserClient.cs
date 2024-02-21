
using StudentResource.Models.POCO;

namespace StudentResource.ApiClients.Interfaces
{
    public interface IUserClient
    {
        Task<User> GetUserByIdAsync(int id);
    }
}
