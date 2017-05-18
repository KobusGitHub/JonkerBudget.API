using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public interface INotificationTaskApplicationService
    {
        Task<IEnumerable<NotificationTaskDtoOut>> GetNotificationTasks();
        Task<IEnumerable<NotificationTaskDtoOut>> GetOpenNotificationTasks();
        Task<NotificationTaskDtoOut> GetNotificationTask(int id);
        Task<NotificationTaskDtoOut> AddNotificationTask(NotificationTaskDtoIn notificationTask);
    }
}
