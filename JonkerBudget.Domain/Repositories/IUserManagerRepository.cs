using DragonFire.Core.Repository;
using JonkerBudget.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Repositories
{
    public interface IUserManagerRepository : IRepository<UserManager>
    {
        Task<UserManager> InsertUserManager(UserManager userManager);
        Task<UserManager> UpdateManager(UserManager userManager);
        Task<IEnumerable<UserManager>> GetUserManagers();
        Task<UserManager> GetUserManager(int Id);
        Task DeleteUserManager(int Id);
    }
}
