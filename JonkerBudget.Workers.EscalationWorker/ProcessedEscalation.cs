using System;
using JonkerBudget.WorkerAPI.Client.Models;

namespace JonkerBudget.Workers
{
    internal class ProcessedEscalation
    {
        public string Message { get; set; }
        public EscalationModel Escalation { get; set; }
        public DateTime SubmitDateTimeUtc { get; set; }
        public bool Success { get; set; }
    }
}