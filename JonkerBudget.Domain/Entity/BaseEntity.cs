using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Entity
{
    public class BaseEntity: Entity
    {
        public BaseEntity()
        {
            this.DateCreatedUtc = DateTime.UtcNow;
            this.DateUpdatedUtc = DateTime.UtcNow;
        }

        public DateTime DateCreatedUtc { get; set; }
        public DateTime DateUpdatedUtc { get; set; }
    }
}
