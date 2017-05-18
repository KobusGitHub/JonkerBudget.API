using JonkerBudget.Application.Dto.Users.Dto;
using JonkerBudget.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.NotificationTasks.Dto.Out
{
    public class NotificationTaskDtoIn
    {
        public int StatusId { get; set; }

        public string CreatedBy { get; set; }

        public string Description { get; set; }
        public int Priority { get; set; }

        public string ImpactDescription { get; set; }

        public string SystemInfo { get; set; }

        public string AssignedToUserId { get; set; }

        public UserDtoOut AssignedTo { get; set; }
    }
}
