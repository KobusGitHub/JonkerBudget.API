using DragonFire.Core.EntityFramework;
using DragonFire.Core.EntityFramework.Providers;

namespace JonkerBudget.EntityFramework.Providers
{
    public class DbContextProvider<TDbContext> : IDbContextProvider<DataContext>
    where TDbContext : DataContext
    {
        public DataContext DbContext
        {
            get
            {
                return (DataContext)_currentUnitOfWorkProvider.Current.GetOrCreateDbContext();
            }
        }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public DbContextProvider()
        {                        
        }

        public DbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }
}
