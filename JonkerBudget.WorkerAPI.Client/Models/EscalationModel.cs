namespace JonkerBudget.WorkerAPI.Client.Models
{
    public class EscalationModel
    {
        public int Id { get; set; }

        public UserModel User { get; set; }

        public int SequenceNo { get; set; }

        public NotificationTaskModel NotificationTask { get; set; }
    }
}
