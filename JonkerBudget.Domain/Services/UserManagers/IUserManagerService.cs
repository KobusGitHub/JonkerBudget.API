using JonkerBudget.Domain.Models.UserManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.UserManagers
{
    public interface IUserManagerService 
    {
        Task<UserManagerModel> InsertUserManagerAsync(UserManagerModel userManager);
        Task<UserManagerModel> UpdateUserManagerAsync(UserManagerModel userManager);
        Task<IEnumerable<UserManagerModel>> GetUserManagersAsync();
        Task<UserManagerModel> GetUserManagerAsync(int Id);
        Task DeleteUserManagerAsync(int Id);
    }
}
