using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.NotificationTaskUpdates
{
    public class NotificationTaskUpdateService : DomainService, INotificationTaskUpdateService
    {

        private IMapper mapper;
        private IRepository<NotificationTaskUpdate> notificationTaskUpdateRepository;
        private IRepository<NotificationTask> notificationTaskRepository;
        IRepository<Status> statusRepository;


        private IUnitOfWork unitOfWork;

        public NotificationTaskUpdateService(IRequestInfoProvider requestInfoProvider, 
            IRepository<NotificationTaskUpdate> notificationTaskUpdateRepository, 
            IRepository<NotificationTask> notificationTaskRepository, 
            IRepository<Status> statusRepository, 
            IMapper mapper, IUnitOfWork unitOfWork) : base(requestInfoProvider)
        {
            this.notificationTaskUpdateRepository = notificationTaskUpdateRepository;
            this.notificationTaskRepository = notificationTaskRepository;
            this.statusRepository = statusRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<NotificationTaskUpdateModel>> GetNotificationTaskUpdates(int notificationTaskId)
        {
            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();
            var notificatinTasks = notificationTaskUpdateRepository.FindWith(n => n.User).ToList();

            return mapper.Map<List<NotificationTaskUpdateModel>>(notificatinTasks.Where(x => x.NotificationTaskId == notificationTaskId));
        }

        public virtual async Task<NotificationTaskUpdateModel> CreateNotificationTaskUpdate(CreateNotificationTaskUpdateModel createNotificationTaskUpdateModel)
        {
            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();

            NotificationTaskUpdate taskUpdate = new NotificationTaskUpdate
            {
                Comment = createNotificationTaskUpdateModel.Comment,
                DateCreatedUtc = DateTime.UtcNow,
                DateUpdatedUtc = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                NotificationTaskId = createNotificationTaskUpdateModel.NotificationTaskId,
                UserId = createNotificationTaskUpdateModel.UserId
            };

            var returnValue = await notificationTaskUpdateRepository.InsertAsync(taskUpdate);
            await unitOfWork.SaveChangesAsync();

            var newTaskUpdate = notificationTaskUpdateRepository.FindWith(n => n.User).FirstOrDefault(x => x.Id == returnValue.Id);

            return mapper.Map<NotificationTaskUpdateModel>(newTaskUpdate);
        }

        public virtual async Task<NotificationTaskUpdateModel> CreateNotificationTaskStatusUpdate(CreateNotificationTaskStatusUpdateModel createNotificationTaskStatusUpdateModel)
        {
            var st = await this.statusRepository.FirstOrDefaultAsync(createNotificationTaskStatusUpdateModel.StatusId);

            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();

            //var task = this.notificationTaskRepository.FindWith(f => f.Status).FirstOrDefault(x => x.Id == createNotificationTaskStatusUpdateModel.NotificationTaskId);
            var task = await this.notificationTaskRepository.FirstOrDefaultAsync(x => x.Id == createNotificationTaskStatusUpdateModel.NotificationTaskId);
            task.StatusId = createNotificationTaskStatusUpdateModel.StatusId;


            NotificationTaskUpdate taskUpdate = new NotificationTaskUpdate
            {
                Comment = "Status changed to " + st.Name,
                DateCreatedUtc = DateTime.UtcNow,
                DateUpdatedUtc = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                NotificationTaskId = createNotificationTaskStatusUpdateModel.NotificationTaskId,
                UserId = createNotificationTaskStatusUpdateModel.UserId
            };

            var returnValue = await notificationTaskUpdateRepository.InsertAsync(taskUpdate);
            
            await unitOfWork.SaveChangesAsync();

            var newTaskUpdate = notificationTaskUpdateRepository.FindWith(n => n.User).FirstOrDefault(x => x.Id == returnValue.Id);

            return mapper.Map<NotificationTaskUpdateModel>(newTaskUpdate);
        }


    }
}
