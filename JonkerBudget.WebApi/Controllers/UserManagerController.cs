using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.UserManagers.Dto;
using JonkerBudget.Domain.Models.UserManagers;
using JonkerBudget.Domain.Services.UserManagers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;


namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/UserManager")]
    public class UserManagerController : ControllerBase
    {
        private IMapper mapper;
        private IUserManagerService userManagerService;

        public UserManagerController(
                IUserManagerService userManagerService,
                IMapper mapper,
                IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.userManagerService = userManagerService;
            this.mapper = mapper;
        }


        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]UserManagerDto userManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await userManagerService.InsertUserManagerAsync(mapper.Map<UserManagerModel>(userManager));
            return Ok(created);
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody]UserManagerDto userManager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await UserManagerExists(userManager.Id))
            {
                return NotFound();
            }

            var updated = await userManagerService.UpdateUserManagerAsync(mapper.Map<UserManagerModel>(userManager));

            return Ok(updated);
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Read()
        {
            var result = mapper.Map<IEnumerable<UserManagerDto>>(await userManagerService.GetUserManagersAsync());
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Read(int id)
        {
            var result = mapper.Map<UserManagerDto>(await userManagerService.GetUserManagerAsync(id));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (!await UserManagerExists(id))
            {
                return NotFound();
            }

            await userManagerService.DeleteUserManagerAsync(id);
            return NoContent();
        }

        private async Task<bool> UserManagerExists(int id)
        {
            var userManager = await userManagerService.GetUserManagerAsync(id);
            if (userManager == null)
            {
                return false;
            }
            return true;
        }





    }
}