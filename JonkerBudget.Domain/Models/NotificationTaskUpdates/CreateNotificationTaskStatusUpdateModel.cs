using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.NotificationTaskUpdates
{
    public class CreateNotificationTaskStatusUpdateModel
    {
        public int StatusId { get; set; }

        public int NotificationTaskId { get; set; }
        public string UserId { get; set; }
    }
}
