using JonkerBudget.Domain.Models;
using JonkerBudget.Domain.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Users
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel> GetAdUserAsync(string userName);
        Task<UserModel> GetUserAsync(string Id);
        Task<UserModel> UpdateUserAsync(UserModel user);
        Task<bool> UpdateUserAsync(string playerId);
        Task<bool> DeleteUserAsync(string Id);
    }
}
