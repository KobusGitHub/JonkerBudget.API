using JonkerBudget.Domain.Models.NotificationTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.NotificationTasks
{
    public interface INotificationTaskService
    {
        Task<IEnumerable<NotificationTaskModelOut>> GetNotificationTasks();
        Task<NotificationTaskModelOut> GetNotificationTask(int id);
        Task<NotificationTaskModelOut> AddNotificationTask(NotificationTaskModel notificationTaskModel);
        Task<IEnumerable<NotificationTaskModelOut>> GetOpenNotificationTasks(string currentUserId);
    }
}
