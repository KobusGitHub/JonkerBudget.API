using JonkerBudget.Domain.Entity;
using JonkerBudget.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Entities
{
    public class EscalationDetail : BaseEntity
    {
        public int PeriodInMinutes { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int SequenceNo { get; set; }

        public int NotificationTaskId { get; set; }

        public DateTime? DateEscalatedUtc { get; set; }

        [ForeignKey("NotificationTaskId")]
        public NotificationTask NotificationTask { get; set; }
    }
}
