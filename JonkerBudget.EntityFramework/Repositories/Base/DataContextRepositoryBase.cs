using DragonFire.Core.Entity;
using DragonFire.Core.EntityFramework.Providers;
using DragonFire.Core.Repository;

namespace JonkerBudget.EntityFramework.Base
{
    public class DataContextRepositoryBase<TEntity> : DataContextRepositoryBase<TEntity, int>, IRepository<TEntity>
    where TEntity : class, IEntity<int>
    {
        public DataContextRepositoryBase(IDbContextProvider<DataContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public class DataContextRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<DataContext, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        public DataContextRepositoryBase(IDbContextProvider<DataContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
