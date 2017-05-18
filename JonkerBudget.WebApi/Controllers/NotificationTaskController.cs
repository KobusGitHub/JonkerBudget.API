using AutoMapper;
using DragonFire.Core.Request;
using Microsoft.AspNet.Identity;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using JonkerBudget.Application.Services.TaskNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/NotificationTasks")]
    public class NotificationTaskController : ControllerBase
    {
        private IMapper mapper;
        private INotificationTaskApplicationService notificationService;

        public NotificationTaskController(
                INotificationTaskApplicationService notificationService,
                IMapper mapper,
                IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.notificationService = notificationService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetNotificationTasks")]
        public async Task<IHttpActionResult> GetNotificationTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            IEnumerable<NotificationTaskDtoOut> tasks = new List<NotificationTaskDtoOut>();
            try
            {
                tasks = await notificationService.GetNotificationTasks();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (tasks == null)
                return BadRequest();

            return Ok(tasks);

        }

        [Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetOpenNotificationTasks")]
        public async Task<IHttpActionResult> GetOpenNotificationTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            IEnumerable<NotificationTaskDtoOut> tasks = new List<NotificationTaskDtoOut>();
            try
            {
                tasks = await notificationService.GetOpenNotificationTasks();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (tasks == null)
                return BadRequest();

            return Ok(tasks);

        }


        [HttpPost]
        [Route("AddNotificationTask")]
        public async Task<IHttpActionResult> AddNotificationTask([FromBody]NotificationTaskDtoIn notificationTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = new NotificationTaskDtoOut();
            //task.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            task.CreatedBy = "Website";

            try
            {
                task = await notificationService.AddNotificationTask(notificationTask);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (task == null)
                return BadRequest();

            return Ok(task);

        }



        [Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetNotificationTask")]
        public async Task<IHttpActionResult> GetNotificationTask(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            NotificationTaskDtoOut task = new NotificationTaskDtoOut();
            try
            {
                task = await notificationService.GetNotificationTask(id);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (task == null)
                return BadRequest();

            return Ok(task);

        }


    }
}
