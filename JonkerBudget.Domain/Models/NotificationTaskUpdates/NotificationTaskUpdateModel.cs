using JonkerBudget.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.NotificationTaskUpdates
{
    public class NotificationTaskUpdateModel
    {
        public int Id { get; set; }
        public int NotificationTaskId { get; set; }
        public UserModel User { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreatedUtc { get; set; }
    }
}
