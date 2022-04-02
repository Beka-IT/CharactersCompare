


using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    public interface IUserService
    {
        Task SaveChangesAsync();
        Task<List<User>> GetAllUsersAsync();
        List<User> GetAllUsers();
        Task<User> GetUserAsync(int id);

        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
        
    }
}