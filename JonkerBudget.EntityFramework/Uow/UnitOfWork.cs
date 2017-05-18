using DragonFire.Core.EntityFramework.Uow;
using EntityFramework.Filters;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using DragonFire.Core.Entity;
using JonkerBudget.EntityFramework.Auditing;
using System.Threading;

namespace JonkerBudget.EntityFramework.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext context;
        private List<Tuple<string, DbEntityEntry, Audit>> insertedEntities;
        private const string IdPropertyName = "Id";

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }

        public DbContext GetOrCreateDbContext()
        {
            if (context == null)
            {
                context = new DataContext();

                //Enable filters here
                context.EnableFilter("IsDeleted");
            }

            return context;
        }

        public int SaveChanges()
        {
            CheckForChanges();
            var result = context.SaveChanges();
            CheckForInsertedEntities();
            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            CheckForChanges();
            var result = await context.SaveChangesAsync();
            await CheckForInsertedEntitiesAsync();
            return result;
        }

        public void CheckForChanges()
        {
            if (context.ChangeTracker.HasChanges())
            {
                insertedEntities = new List<Tuple<string, DbEntityEntry, Audit>>();
                AuditEntries(context.ChangeTracker.Entries().ToList());               
            }
        }

        public void CheckForInsertedEntities()
        {

            if (insertedEntities != null)
            { 
                UpdateInsertedEntityIds();
                insertedEntities.Clear();
            }
        }

        public async Task CheckForInsertedEntitiesAsync()
        {
            if (insertedEntities != null)
            {
                await UpdateInsertedEntityIdsAsync();
                insertedEntities.Clear();
            }
        }

        private void UpdateInsertedEntityIds()
        {
            if (insertedEntities != null && insertedEntities.Count > 0)
            {
                foreach (var item in insertedEntities)
                {
                    var auditEntry = item.Item3;
                    auditEntry.ObjectId = item.Item2.CurrentValues[IdPropertyName].ToString();
                }

                context.SaveChanges();
            }
        }

        private async Task UpdateInsertedEntityIdsAsync()
        {
            if (insertedEntities != null && insertedEntities.Count > 0)
            {
                foreach (var item in insertedEntities)
                {
                    var auditEntry = item.Item3;
                    auditEntry.ObjectId = item.Item2.CurrentValues[IdPropertyName].ToString();
                }

                await context.SaveChangesAsync();
            }
        }

        private void AuditEntries(List<DbEntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                var type = entry.Entity.GetType();

                if (entry.State == EntityState.Added)
                {
                    if (ShouldAuditEntry(type, EntityState.Added))
                    {
                        AuditInsert(type, entry);
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (ShouldAuditEntry(type, EntityState.Modified))
                    {
                        AuditUpdate(type, entry);
                    }
                }

                if (entry.State == EntityState.Deleted)
                {
                    if (ShouldAuditEntry(type, EntityState.Deleted))
                    {
                        AuditDelete(type, entry);
                    }
                }
            }
        }

        private void AuditInsert(Type type, DbEntityEntry entry)
        {
            string tempId = Guid.NewGuid().ToString();

            var audit = CreateAudit(type, entry, 0, tempId); // - Insert

            insertedEntities.Add(new Tuple<string, DbEntityEntry, Audit>(tempId.ToString(), entry, audit));
        }

        private void AuditUpdate(Type type, DbEntityEntry entry)
        {
            if (PerformingSoftDelete(type, entry))
                CreateAudit(type, entry, 3, GetEntityId(entry)); // - Soft delete           
            else
                CreateAudit(type, entry, 1, GetEntityId(entry)); // - Update
        }

        private void AuditDelete(Type type, DbEntityEntry entry)
        {
            CreateAudit(type, entry, 2, GetEntityId(entry, true)); // - Delete
        }

        private bool PerformingSoftDelete(Type type, DbEntityEntry entry)
        {
            var assignable = typeof(ISoftDelete).IsAssignableFrom(type);

            if (!assignable)
                return false;

            if (assignable)
            {
                //Check the IsDeleted property and check if it was 0 and is being set to 1
                var oldVal = entry.OriginalValues["IsDeleted"];
                var newVal = entry.CurrentValues["IsDeleted"];

                if ((bool)oldVal == false && (bool)newVal == true)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetEntityId(DbEntityEntry entry, bool original = false)
        {
            object id = null;

            if (!original)
                id = entry.CurrentValues[IdPropertyName];
            else
                id = entry.OriginalValues[IdPropertyName];

            return id.ToString();
        }

        private Audit CreateAudit(Type type, DbEntityEntry entry, int status, string objectId)
        {
            //Handle proxy base types
            string typeName = string.Empty;

            if (type.BaseType.Name == "Entity")
                typeName = type.Name;
            else
                typeName = type.BaseType.Name;

            var auditDetails = CreateAuditDetails(entry, entry.State);

            return context.Audits.Add(new Audit
            {
                TableName = typeName,
                ActionId = status,
                TimeStampUtc = DateTime.UtcNow,
                AuditDetails = auditDetails,
                ObjectId = objectId,
                Username = GetUsername()
            });
        }

        private string GetUsername()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(userName)) return "Anonymous";
            return userName;
        }

        private List<AuditDetail> CreateAuditDetails(DbEntityEntry entry, EntityState entityState)
        {
            List<AuditDetail> auditDetails = new List<AuditDetail>();
            List<string> properties = new List<string>();

            if (entityState == EntityState.Deleted)
                properties = entry.OriginalValues.PropertyNames.ToList();
            else
                properties = entry.CurrentValues.PropertyNames.ToList();

            foreach (var propertyName in properties.Where(name => name != IdPropertyName))
            {
                object curVal = null;

                if (entityState == EntityState.Deleted)
                    curVal = entry.OriginalValues[propertyName];
                else
                    curVal = entry.CurrentValues[propertyName];

                object oldVal = null;

                if (entityState != EntityState.Added)
                {
                    if (entry.OriginalValues != null)
                        oldVal = entry.OriginalValues[propertyName];
                }

                if (NotEqual(oldVal, curVal))
                {
                    auditDetails.Add(new AuditDetail()
                    {
                        PropertyName = propertyName,
                        OldValue = oldVal == null ? null : oldVal.ToString(),
                        NewValue = curVal == null ? null : curVal.ToString()
                    });
                }
            }

            return auditDetails;
        }

        private bool NotEqual(object oldVal, object curVal)
        {
            if (oldVal == null && curVal != null)
                return true;
            if (oldVal != null && curVal == null)
                return true;
            if (oldVal == null && curVal == null)
                return false;

            var oldValType = oldVal.GetType().FullName;
            var curValType = oldVal.GetType().FullName;

            var oldValue = Convert.ChangeType(oldVal, Type.GetType(oldValType));
            var curValue = Convert.ChangeType(curVal, Type.GetType(curValType));

            if (oldValue.ToString() != curValue.ToString())
                return true;

            return false;
        }

        private bool ShouldAuditEntry(Type type, EntityState state)
        {
            var auditEntityAttribute = Attribute.GetCustomAttribute(type, typeof(AuditEntity));

            if (auditEntityAttribute == null)
                return false;

            var dontAuditEntityAttribute = Attribute.GetCustomAttribute(type, typeof(DontAuditEntity));
            if (dontAuditEntityAttribute != null)
                return false;

            AuditEntity auditAttribute = ((AuditEntity)auditEntityAttribute);

            if (auditAttribute.auditTypes == null || auditAttribute.auditTypes.Length == 0)
                return true;

            if (state == EntityState.Added)
            {
                if (auditAttribute.auditTypes.Contains((AuditEntityType)1))
                    return true;
            }

            if (state == EntityState.Modified)
            {
                if (auditAttribute.auditTypes.Contains((AuditEntityType)2))
                    return true;
            }

            if (state == EntityState.Deleted)
            {
                if (auditAttribute.auditTypes.Contains((AuditEntityType)3))
                    return true;
            }

            return true;
        }
    }
}
