using System;
using System.Linq;
using DragonFire.Core.EntityFramework.Providers;
using JonkerBudget.Domain.Identity;
using JonkerBudget.Domain.Repositories;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        DataContext context;
        public UserRepository(IDbContextProvider<DataContext> dbContextProvider)            
        {
            context = dbContextProvider.DbContext;
        }

        public Task DeleteUser(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAdUser(string userName)
        {
            return await context.Users
                 .Where(e => e.UserName == userName)
                 .FirstOrDefaultAsync();
        }

        public IQueryable<User> GetAll()
        {
            return context.Users;
        }

        public IQueryable<User> GetAll(params Expression<Func<User, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(GetAll(), (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<User> GetAll(params string[] includeProperties)
        {
            return includeProperties.Aggregate(GetAll(), (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<User> GetUser(string Id)
        {
            return await context.Users
                 .Where(e => e.Id == Id)
                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users
                 .Where(e => e.LockoutEnabled == false)
                 .ToListAsync();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}