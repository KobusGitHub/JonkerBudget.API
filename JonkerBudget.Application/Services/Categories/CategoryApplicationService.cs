using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.Categories.Dto.In;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using JonkerBudget.Application.Services.Base;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Services.EscalationDetails;
using JonkerBudget.Domain.Services.NotificationTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public class CategoryApplicationService : ApplicationService, ICategoryApplicationService
    {

        private IMapper mapper;
        private ICategoryService categoryService;
        private IRequestInfoProvider requestInfoProvider;

        public CategoryApplicationService(IMapper mapper, ICategoryService categoryService, IRequestInfoProvider requestInfoProvider) : base (requestInfoProvider)
        {
            this.requestInfoProvider = requestInfoProvider;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<CategoryDtoOut> AddCategory(CreateCategoryDtoIn createCategoryDtoIn)
        {
            var model = this.mapper.Map<CategoryModel>(createCategoryDtoIn);
            var task = await categoryService.AddCategory(model);
            var dto = this.mapper.Map<CategoryDtoOut>(task);
            return dto;
        }

        public async Task<IEnumerable<CategoryDtoOut>> GetAllCategories()
        {
            //var currentUserId = requestInfoProvider.Current.UserId;
            var expenseModels = await categoryService.GetAllCategories();
            var dtos = this.mapper.Map<IEnumerable<CategoryDtoOut>>(expenseModels);

            return dtos;
        }

    }
}
