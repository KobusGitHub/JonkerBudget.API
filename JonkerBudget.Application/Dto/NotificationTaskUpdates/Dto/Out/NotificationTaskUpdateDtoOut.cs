using JonkerBudget.Application.Dto.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.Out
{
    public class NotificationTaskUpdateDtoOut
    {
        public int id { get; set; }
        public int NotificationTaskId { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreatedUtc { get; set; }
        public UserDtoOut User { get; set; }
    }
}
