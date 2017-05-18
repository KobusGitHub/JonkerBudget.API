using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Repositories;
using JonkerBudget.EntityFramework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFire.Core.EntityFramework.Providers;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace JonkerBudget.EntityFramework.Repositories
{
    public class UserManagerRepository : DataContextRepositoryBase<UserManager>, IUserManagerRepository
    {
        IDbContextProvider<DataContext> dbContextProvider;

        public UserManagerRepository(IDbContextProvider<DataContext> dbContextProvider) : base(dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        public Task DeleteUserManager(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserManager> GetUserManager(int Id)
        {
            return await Context.UserManagers
                .Where(e => e.Id == Id)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<UserManager>> GetUserManagers()
        {
            return await Context.UserManagers
                .ToListAsync();
        }

        public Task<UserManager> InsertUserManager(UserManager userManager)
        {
            throw new NotImplementedException();
        }

        public Task<UserManager> UpdateManager(UserManager userManager)
        {
            throw new NotImplementedException();
        }
    }
}
