using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.EscalationDetails
{
    public class EscalationDetailModel
    {
        public int Id { get; set; }
        public int PeriodInMinutes { get; set; }
        public string UserId { get; set; }
        
        public UserModel User { get; set; }
             
        public int SequenceNo { get; set; }

        public int NotificationTaskId { get; set; }
    }
}
