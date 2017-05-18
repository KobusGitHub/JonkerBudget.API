using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.NotificationTaskUpdates
{
    public interface INotificationTaskUpdateService
    {
        Task<IEnumerable<NotificationTaskUpdateModel>> GetNotificationTaskUpdates(int notificationTaskId);
        Task<NotificationTaskUpdateModel> CreateNotificationTaskUpdate(CreateNotificationTaskUpdateModel createNotificationTaskUpdateModel);
        Task<NotificationTaskUpdateModel> CreateNotificationTaskStatusUpdate(CreateNotificationTaskStatusUpdateModel createNotificationTaskStatusUpdateModel);
    }
}
