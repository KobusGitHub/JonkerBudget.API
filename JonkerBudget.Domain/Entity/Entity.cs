using DragonFire.Core.Entity;
using System;

namespace JonkerBudget.Domain.Entity
{
    /// <summary>
    /// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public abstract class Entity : Entity<int>, IEntity, IActive , ISoftDelete
    {
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
