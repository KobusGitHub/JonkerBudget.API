using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.NotificationTasks
{
    public class NotificationTaskService : DomainService, INotificationTaskService
    {
        private IMapper mapper;
        private IRepository<NotificationTask> notificationTaskRepository;
        private IUnitOfWork unitOfWork;

        public NotificationTaskService(IRequestInfoProvider requestInfoProvider, IRepository<NotificationTask> notificationTaskRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(requestInfoProvider)
        {
            this.notificationTaskRepository = notificationTaskRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<NotificationTaskModelOut> AddNotificationTask(NotificationTaskModel notificationTaskModel)
        {
            var newNotificationTask = await notificationTaskRepository.InsertAsync(mapper.Map<NotificationTask>(notificationTaskModel));
            await unitOfWork.SaveChangesAsync();

            var returnModel = mapper.Map<NotificationTaskModelOut>(newNotificationTask);

            return returnModel;
        }

        public virtual async Task<NotificationTaskModelOut> GetNotificationTask(int id)
        {
            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();
            //var notificatinTasks = notificationTaskRepository.FindWith(n => n.AssignedTo, s => s.Status, e => e.EscalationDetails).ToList();
            var notificatinTasks = notificationTaskRepository.FindWith(
                "AssignedTo", "Status", "EscalationDetails", "EscalationDetails.User"
                ).ToList();

            return mapper.Map<NotificationTaskModelOut>(notificatinTasks.FirstOrDefault(x => x.Id == id));
        }

        public virtual async Task<IEnumerable<NotificationTaskModelOut>> GetNotificationTasks()
        {
            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();
            var notificatinTasks = await notificationTaskRepository.FindWith(n => n.AssignedTo, s => s.Status).ToListAsync();

            return mapper.Map<List<NotificationTaskModelOut>>(notificatinTasks);
        }

        public virtual async Task<IEnumerable<NotificationTaskModelOut>> GetOpenNotificationTasks(string currentUserId)
        {
            //var notificatinTasks = await notificationTaskRepository.GetAllListAsync();
            var notificatinTasks = await notificationTaskRepository.FindWith(n => n.AssignedTo, s => s.Status).Where(x => x.StatusId != 6 && x.AssignedToUserId == currentUserId).ToListAsync();

            return mapper.Map<List<NotificationTaskModelOut>>(notificatinTasks);
        }



    }
}
