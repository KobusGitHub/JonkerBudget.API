using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.NotificationTaskUpdates
{
    public class CreateNotificationTaskUpdateModel
    {
        public int NotificationTaskId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
       

    }
}
