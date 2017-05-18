using JonkerBudget.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string Id);
        Task<User> GetAdUser(string userName);
        Task<User> UpdateUser(User user);
        Task DeleteUser(string Id);
        IQueryable<User> GetAll();
        IQueryable<User> GetAll(params Expression<Func<User, object>>[] includeProperties);
        IQueryable<User> GetAll(params string[] includeProperties);
    }
}
