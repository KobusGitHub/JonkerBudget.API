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
using JonkerBudget.Domain.Services.EscalationDetails;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Dto.Categories.Dto.In;

namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoryController : ControllerBase
    {
        private IMapper mapper;
        private ICategoryApplicationService categoryService;

        public CategoryController(
            ICategoryApplicationService categoryService,
            IMapper mapper,
            IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        //[Authorize(Roles = "User, Administrator")]
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IHttpActionResult> GetAllCategories()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<CategoryDtoOut> categories = new List<CategoryDtoOut>();
            try
            {
                categories = await categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }

       
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IHttpActionResult> AddCategory([FromBody]CreateCategoryDtoIn createCategoryDtoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new CategoryDtoOut();
            //task.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            try
            {
                category = await categoryService.AddCategory(createCategoryDtoIn);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            if (category == null)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        [HttpPost]
        [Route("AddCategories")]
        public async Task<IHttpActionResult> AddCategories([FromBody]List<CreateCategoryDtoIn> createCategorytDtoInList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //task.CreatedBy = HttpContext.Current.User.Identity.GetUserId();
            try
            {
                await categoryService.AddCategories(createCategorytDtoInList);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }



            return Ok();
        }

    }
}
