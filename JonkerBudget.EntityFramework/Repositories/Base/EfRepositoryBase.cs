using DragonFire.Core.Entity;
using DragonFire.Core.EntityFramework.Providers;
using DragonFire.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JonkerBudget.EntityFramework.Base
{
    public class EfRepositoryBase<TDbContext, TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey> , IDisposable
      where TEntity : class, IEntity<TPrimaryKey>
      where TDbContext : DataContext
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        protected virtual DbSet<TEntity> Table { get { return Context.Set<TEntity>(); } }

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public EfRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;           
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public override IQueryable<TEntity> FindWith()
        {
            IQueryable<TEntity> query = Table;
            return query;
        }

        public override IQueryable<TEntity> FindWith(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(Table, (current, includeProperty) => current.Include(includeProperty));
        }

        public override IQueryable<TEntity> FindWith(params string[] includeProperties)
        {
            return includeProperties.Aggregate<string, IQueryable<TEntity>>(Table, (current, includeProperty) => current.Include(includeProperty));
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await Task.FromResult(Table.Add(entity));
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return await Task.FromResult(entity);
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);

            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
            }
            else
            {
                Table.Remove(entity);
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
            if (entity == null)
            {
                entity = FirstOrDefault(id);
                if (entity == null)
                {
                    return;
                }
            }

            Delete(entity);
        }

        public override async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).CountAsync();
        }

        public override async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).LongCountAsync();
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        public override bool Any(TPrimaryKey id)
        {
            return Table.Any(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
        }

        public override Task<bool> AnyAsync(TPrimaryKey id)
        {
            return Table.AnyAsync(ent => EqualityComparer<TPrimaryKey>.Default.Equals(ent.Id, id));
        }

        public override Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = includeProperties.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(Table, (current, includeProperty) => current.Include(includeProperty));
            query = query.Where(predicate);
            return query.ToListAsync();
        }

        public override Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            var query = includeProperties.Aggregate<string, IQueryable<TEntity>>(Table, (current, includeProperty) => current.Include(includeProperty));
            query = query.Where(predicate);
            return query.ToListAsync();
        }

        public override IQueryable<TEntity> FindWithAsNoTracking()
        {
            return FindWith().AsNoTracking();        
        }

        public override IQueryable<TEntity> FindWithAsNoTracking(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindWith(includeProperties).AsNoTracking();
        }

        public override IQueryable<TEntity> FindWithAsNoTracking(params string[] includeProperties)
        {
            return FindWith(includeProperties).AsNoTracking();            
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}
