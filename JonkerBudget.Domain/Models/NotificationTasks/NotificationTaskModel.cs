using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.Statuses;
using JonkerBudget.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.NotificationTasks
{
    public class NotificationTaskModel
    {
        public int id { get; set; }
        public int StatusId { get; set; }

        public StatusModel Status { get; set; }

        public string CreatedBy { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public string ImpactDescription { get; set; }

        public string SystemInfo { get; set; }

        public string AssignedToUserId { get; set; }

        public UserModel AssignedTo { get; set; }

        public ICollection<EscalationDetailModel> EscalationDetails { get; set; }

    }
}
