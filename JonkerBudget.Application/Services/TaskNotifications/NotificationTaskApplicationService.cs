using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using JonkerBudget.Application.Services.Base;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Services.NotificationTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public class NotificationTaskApplicationService : ApplicationService, INotificationTaskApplicationService
    {

        private IMapper mapper;
        private INotificationTaskService notificationTaskService;
        private IRequestInfoProvider requestInfoProvider;

        public NotificationTaskApplicationService(IMapper mapper, INotificationTaskService notificationTaskService, IRequestInfoProvider requestInfoProvider) : base (requestInfoProvider)
        {
            this.requestInfoProvider = requestInfoProvider;
            this.notificationTaskService = notificationTaskService;
            this.mapper = mapper;
        }

        public async Task<NotificationTaskDtoOut> AddNotificationTask(NotificationTaskDtoIn notificationTask)
        {
            var model = this.mapper.Map<NotificationTaskModel>(notificationTask);
            var task = await notificationTaskService.AddNotificationTask(model);
            var dto = this.mapper.Map<NotificationTaskDtoOut>(task);
            return dto;
        }

        public async Task<IEnumerable<NotificationTaskDtoOut>> GetOpenNotificationTasks()
        {
            var currentUserId = requestInfoProvider.Current.UserId;
            var notificationTasks = await notificationTaskService.GetOpenNotificationTasks(currentUserId);
            var dtos = this.mapper.Map<IEnumerable<NotificationTaskDtoOut>>(notificationTasks);

            foreach (var dto in dtos)
            {
                dto.Status = notificationTasks.FirstOrDefault(x => x.id == dto.id).Status.Name;
            }


            return dtos;
        }
        public async Task<IEnumerable<NotificationTaskDtoOut>> GetNotificationTasks()
        {
            var notificationTasks = await notificationTaskService.GetNotificationTasks();
            var dtos = this.mapper.Map<IEnumerable<NotificationTaskDtoOut>>(notificationTasks);

            foreach (var dto in dtos)
            {
                dto.Status = notificationTasks.FirstOrDefault(x => x.id == dto.id).Status.Name;
            }
            
            return dtos;
        }

        public async Task<NotificationTaskDtoOut> GetNotificationTask(int id)
        {
            var notificationTask = await notificationTaskService.GetNotificationTask(id);
            var dto = this.mapper.Map<NotificationTaskDtoOut>(notificationTask);
            dto.Status = notificationTask.Status.Name;
            

            return dto;
        }

    }
}
