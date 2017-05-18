using JonkerBudget.Domain.Entity;
using JonkerBudget.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Entities
{
    public class NotificationTaskUpdate: BaseEntity
    {
        public int NotificationTaskId { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [StringLength(2000)]
        public string Comment { get; set; }
    }
}
