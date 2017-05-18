using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Services.Base;
using JonkerBudget.Domain.Services.NotificationTasks;
using JonkerBudget.Domain.Services.NotificationTaskUpdates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public class EscalationDetailService : DomainService, IEscalationDetailService
    {
        private IMapper mapper;
        private IRepository<EscalationDetail> escalationDetailRepository;
        private IUnitOfWork unitOfWork;
        private IRepository<NotificationTask> notificationTaskRepository;
        private INotificationTaskService notificationTaskService;
        private INotificationTaskUpdateService notificationTaskUpdateService;

        public EscalationDetailService(IRequestInfoProvider requestInfoProvider, 
            IRepository<EscalationDetail> escalationDetailRepository,
            IRepository<NotificationTask> notificationTaskRepository,
            INotificationTaskService notificationTaskService,
            INotificationTaskUpdateService notificationTaskUpdateService,
            IMapper mapper, IUnitOfWork unitOfWork) : base(requestInfoProvider)
        {
            this.escalationDetailRepository = escalationDetailRepository;
            this.mapper = mapper;
            this.notificationTaskRepository = notificationTaskRepository;
            this.notificationTaskService = notificationTaskService;
            this.notificationTaskUpdateService = notificationTaskUpdateService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EscalationDetailModelOut>> GetEscalationsBreached()
        {
            // grab all notifications
            var notificationsToProcess = await this.notificationTaskRepository.FindWith("AssignedTo", "EscalationDetails", "EscalationDetails.User").Where(match => match.EscalationDetails.Any() && match.StatusId == 6).ToListAsync();
            var escalationsToReturn = new List<EscalationDetailModelOut>();
            foreach (var notificationToProcess in notificationsToProcess)
            {
                var currentEscalationBreachTime = notificationToProcess.DateCreatedUtc;

                foreach (var escalation in notificationToProcess.EscalationDetails.OrderBy(e=>e.SequenceNo))
                {
                    currentEscalationBreachTime = currentEscalationBreachTime.AddMinutes(escalation.PeriodInMinutes);
                    if(DateTime.UtcNow > currentEscalationBreachTime && escalation.DateEscalatedUtc == null)
                    {
                        var mappedEscalation = mapper.Map<EscalationDetailModelOut>(escalation);
                        mappedEscalation.NotificationTask = mapper.Map<NotificationTaskModelOut>(notificationToProcess);
                        escalationsToReturn.Add(mappedEscalation);
                    }
                }
            }

            return escalationsToReturn;
        }

        public async Task SetEscalationsProcessedAsSuccessAsync(int[] ids)
        {
            foreach (var escalationId in ids)
            {
                var escalation = await this.escalationDetailRepository.FindWith("NotificationTask", "User").FirstOrDefaultAsync(esc => esc.Id == escalationId);                

                if(escalation == null)
                {
                    continue;
                }

                escalation.DateEscalatedUtc = DateTime.UtcNow;

                await this.notificationTaskService.AddNotificationTask(new NotificationTaskModel
                {
                    AssignedToUserId = escalation.UserId,
                    CreatedBy = "Escalation Watchdog",
                    ImpactDescription = escalation.NotificationTask.ImpactDescription,
                    Priority = escalation.NotificationTask.Priority,
                    Description = "Escalation of task: " + escalation.NotificationTask.Description,
                    SystemInfo = escalation.NotificationTask.SystemInfo,                    
                    StatusId = 1
                });

                await this.notificationTaskUpdateService.CreateNotificationTaskUpdate(new CreateNotificationTaskUpdateModel
                    {
                        Comment = "Escalated to " + escalation.User.FirstName + " " + escalation.User.Surname,
                        NotificationTaskId = escalation.NotificationTaskId,
                        UserId = escalation.UserId
                    }
                );
            }

            await this.unitOfWork.SaveChangesAsync();
        }
    }
}
