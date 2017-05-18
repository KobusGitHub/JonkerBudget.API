namespace JonkerBudget.WorkerAPI.Client.Models
{
    public class NotificationTaskModel
    {
        public string Description { get; set; }
        public int Priority { get; set; }

        public string ImpactDescription { get; set; }

        public string SystemInfo { get; set; }

        public UserModel AssignedTo { get; set; }
    }
}