using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Services.TaskNotifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Dto.Categories.Dto.In;

namespace JonkerBudget.WebApi.Controllers
{
    [RoutePrefix("api/Tests")]
    public class TestController : ControllerBase
    {
        private IMapper mapper;
        private ICategoryApplicationService categoryService;

        public TestController(
            IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        { }

        [HttpGet]
        [Route("GetTestCategories")]
        public async Task<IHttpActionResult> GetTestCategories()
        {
            List<CategoryDtoOut> categories = new List<CategoryDtoOut>
            {
                new CategoryDtoOut {Id = 1, CategoryName = "Category1", Budget = 1000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 2, CategoryName = "Category2", Budget = 2000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 3, CategoryName = "Category3", Budget = 3000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 4, CategoryName = "Category4", Budget = 4000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 5, CategoryName = "Category5", Budget = 5000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 6, CategoryName = "Category6", Budget = 6000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 7, CategoryName = "Category7", Budget = 7000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 8, CategoryName = "Category8", Budget = 8000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 9, CategoryName = "Category9", Budget = 9000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 10, CategoryName = "Category10", Budget = 10000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 11, CategoryName = "Category11", Budget = 11000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 12, CategoryName = "Category12", Budget = 12000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 13, CategoryName = "Category13", Budget = 13000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 14, CategoryName = "Category14", Budget = 14000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 15, CategoryName = "Category15", Budget = 15000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 16, CategoryName = "Category16", Budget = 16000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 17, CategoryName = "Category17", Budget = 17000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 18, CategoryName = "Category18", Budget = 18000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 19, CategoryName = "Category19", Budget = 19000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 20, CategoryName = "Category20", Budget = 20000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 21, CategoryName = "Category21", Budget = 21000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 22, CategoryName = "Category22", Budget = 22000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 23, CategoryName = "Category23", Budget = 23000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 24, CategoryName = "Category24", Budget = 24000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 25, CategoryName = "Category25", Budget = 25000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 26, CategoryName = "Category26", Budget = 26000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 27, CategoryName = "Category27", Budget = 27000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 28, CategoryName = "Category28", Budget = 28000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 29, CategoryName = "Category29", Budget = 29000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 30, CategoryName = "Category30", Budget = 30000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 31, CategoryName = "Category31", Budget = 31000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 32, CategoryName = "Category32", Budget = 32000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 33, CategoryName = "Category33", Budget = 33000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 34, CategoryName = "Category34", Budget = 34000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 35, CategoryName = "Category35", Budget = 35000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 36, CategoryName = "Category36", Budget = 36000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 37, CategoryName = "Category37", Budget = 37000, GuidId = Guid.NewGuid() },
                new CategoryDtoOut {Id = 38, CategoryName = "Category38", Budget = 38000, GuidId = Guid.NewGuid() }
            };

            return Ok(categories);
        }

    }
}
