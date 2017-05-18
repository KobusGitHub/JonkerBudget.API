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
    public class UserManager : BaseEntity
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public User Manager { get; set; }
    }
}
