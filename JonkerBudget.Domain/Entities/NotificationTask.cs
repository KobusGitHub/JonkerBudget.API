using DragonFire.Core.Entity;
using JonkerBudget.Domain.Entity;
using JonkerBudget.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JonkerBudget.Domain.Entities
{
    [AuditEntity]
    public class NotificationTask : BaseEntity
    {
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public int Priority { get; set; }

        [StringLength(1000)]
        public string ImpactDescription { get; set; }

        [StringLength(100)]
        public string SystemInfo { get; set; }

        public string AssignedToUserId { get; set; }

        [ForeignKey("AssignedToUserId")]
        public User AssignedTo { get; set; }

        public virtual ICollection<EscalationDetail> EscalationDetails { get; set; }
        public virtual ICollection<NotificationTaskUpdate> NotificationTaskUpdates { get; set; }
    }
}
