using JonkerBudget.Application.Dto.EscalationDetails.Dto.Out;
using JonkerBudget.Application.Dto.Users.Dto;
using JonkerBudget.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.NotificationTasks.Dto.Out
{
    public class NotificationTaskDtoOut
    {
        public int id { get; set; }
        public int StatusId { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreatedUtc { get; set; }

        public string Description { get; set; }
        public int Priority { get; set; }

        public string ImpactDescription { get; set; }

        public string SystemInfo { get; set; }

        public string AssignedToUserId { get; set; }

        public UserDtoOut AssignedTo { get; set; }

        public ICollection<EscalationDetailDtoOut> EscalationDetails { get; set; }



    }
}
