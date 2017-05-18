using JonkerBudget.Application.Dto.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.EscalationDetails.Dto.Out
{
    public class EscalationDetailDtoOut
    {
        public int Id { get; set; }
        public int PeriodInMinutes { get; set; }
        public string UserId { get; set; }
        public UserDtoOut User { get; set; }
        public int SequenceNo { get; set; }

    }
}
