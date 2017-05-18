using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.In;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.Out;
using JonkerBudget.Application.Services.Base;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Services.NotificationTasks;
using JonkerBudget.Domain.Services.NotificationTaskUpdates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotificationUpdates
{
    public class TaskNotificationUpdateApplicationService : ApplicationService, ITaskNotificationUpdateApplicationService
    {

        private IMapper mapper;
        private INotificationTaskUpdateService notificationTaskUpdateService;
        private IRequestInfoProvider requestInfoProvider;

        public TaskNotificationUpdateApplicationService(IMapper mapper, INotificationTaskUpdateService notificationTaskUpdateService, IRequestInfoProvider requestInfoProvider) : base (requestInfoProvider)
        {
            this.requestInfoProvider = requestInfoProvider;
            this.notificationTaskUpdateService = notificationTaskUpdateService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NotificationTaskUpdateDtoOut>> GetNotificationTaskUpdates(int notificationTaskId)
        {
            var notificationTaskUpdates = await notificationTaskUpdateService.GetNotificationTaskUpdates(notificationTaskId);
            var dtos = this.mapper.Map<IEnumerable<NotificationTaskUpdateDtoOut>>(notificationTaskUpdates);
            return dtos;
        }
        public async Task<NotificationTaskUpdateDtoOut> CreateNotificationTaskUpdate(NotificationTaskUpdateDtoIn notificationTaskUpdateDtoIn)
        {
            var currentUserId = requestInfoProvider.Current.UserId;

            var model = this.mapper.Map<CreateNotificationTaskUpdateModel>(notificationTaskUpdateDtoIn);
            model.UserId = currentUserId;
            var notificationTaskUpdate = await notificationTaskUpdateService.CreateNotificationTaskUpdate(model);

            var dtos = this.mapper.Map<NotificationTaskUpdateDtoOut>(notificationTaskUpdate);
            return dtos;
        }

        public async Task<NotificationTaskUpdateDtoOut> CreateNotificationTaskStatusUpdate(NotificationTaskStatusUpdateDtoIn notificationTaskUpdateDtoIn)
        {
            var currentUserId = requestInfoProvider.Current.UserId;
            var model = this.mapper.Map<CreateNotificationTaskStatusUpdateModel>(notificationTaskUpdateDtoIn);
            model.UserId = currentUserId;

            var notificationTaskUpdate = await notificationTaskUpdateService.CreateNotificationTaskStatusUpdate(model);
            var dtos = this.mapper.Map<NotificationTaskUpdateDtoOut>(notificationTaskUpdate);
            return dtos;
        }



    }
}
