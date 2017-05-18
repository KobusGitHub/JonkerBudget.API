using JonkerBudget.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Entities
{
    public class Status: BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
    }
}

