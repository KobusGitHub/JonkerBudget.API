using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JonkerBudget.EntityFramework.Auditing
{
    public class Audit
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public DateTime TimeStampUtc { get; set; }
        public string TableName { get; set; }    
        [MaxLength(128)]
        public string ObjectId { get; set; }    
        public int ActionId { get; set; }
        public ICollection<AuditDetail> AuditDetails { get; set; }
    }
}
