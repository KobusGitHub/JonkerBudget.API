using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.In;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.Out;
using JonkerBudget.Application.Services.TaskNotificationUpdates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/NotificationTaskUpdates")]
    public class NotificationTaskUpdateController : ControllerBase
    {
        private ITaskNotificationUpdateApplicationService notificationUpdateService;

        public NotificationTaskUpdateController(
                ITaskNotificationUpdateApplicationService notificationUpdateService,
                IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.notificationUpdateService = notificationUpdateService;
            
        }

        [Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetNotificationTaskUpdates")]
        public async Task<IHttpActionResult> GetNotificationTaskUpdates(int notificationTaskId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<NotificationTaskUpdateDtoOut> taskUpdates = new List<NotificationTaskUpdateDtoOut>();

            try
            {
                taskUpdates = await notificationUpdateService.GetNotificationTaskUpdates(notificationTaskId);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (taskUpdates == null)
            {
                return BadRequest();
            }

            return Ok(taskUpdates);

        }

        [Authorize(Roles = "User, Administrator")]
        [HttpPost]
        [Route("CreateNotificationTaskUpdate")]
        public async Task<IHttpActionResult> CreateNotificationTaskUpdate(NotificationTaskUpdateDtoIn taskUpdateIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NotificationTaskUpdateDtoOut taskUpdate = null;
            try
            {
                taskUpdate = await notificationUpdateService.CreateNotificationTaskUpdate(taskUpdateIn);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (taskUpdate == null)
            {
                return BadRequest();
            }

            return Ok(taskUpdate);

        }

        [Authorize(Roles = "User, Administrator")]
        [HttpPost]
        [Route("CreateNotificationTaskStatusUpdate")]
        public async Task<IHttpActionResult> CreateNotificationTaskStatusUpdate(NotificationTaskStatusUpdateDtoIn taskUpdateIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NotificationTaskUpdateDtoOut taskUpdate = null;
            try
            {
                taskUpdate = await notificationUpdateService.CreateNotificationTaskStatusUpdate(taskUpdateIn);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (taskUpdate == null)
            {
                return BadRequest();
            }

            return Ok(taskUpdate);

        }

    }
}
