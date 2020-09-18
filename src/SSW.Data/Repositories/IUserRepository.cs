using SSW.Data.Entities;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<string> GetRoleAsync(string email);
    }
}
