using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.In;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotificationUpdates
{
    public interface ITaskNotificationUpdateApplicationService
    {
        Task<IEnumerable<NotificationTaskUpdateDtoOut>> GetNotificationTaskUpdates(int notificationTaskId);
        Task<NotificationTaskUpdateDtoOut> CreateNotificationTaskUpdate(NotificationTaskUpdateDtoIn notificationTaskUpdateDtoIn);
        Task<NotificationTaskUpdateDtoOut> CreateNotificationTaskStatusUpdate(NotificationTaskStatusUpdateDtoIn notificationTaskStatusUpdateDtoIn);
    }
}
