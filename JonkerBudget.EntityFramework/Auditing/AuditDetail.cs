namespace JonkerBudget.EntityFramework.Auditing
{
    public class AuditDetail
    {
        public long Id { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
