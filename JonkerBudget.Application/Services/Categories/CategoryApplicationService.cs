using AutoMapper;
using DragonFire.Core.Request;
using JonkerBudget.Application.Dto.Categories.Dto.In;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Services.Base;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Services.EscalationDetails;
using System.Collections.Generic;
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

        public async Task<CategoryDtoOut> UpdateCategory(CreateCategoryDtoIn createCategoryDtoIn)
        {
            var model = this.mapper.Map<CategoryModel>(createCategoryDtoIn);
            var task = await categoryService.UpdateCategory(model);
            var dto = this.mapper.Map<CategoryDtoOut>(task);
            return dto;
        }

        public async Task AddCategories(List<CreateCategoryDtoIn> createCategoryDtoInList)
        {

            var models = this.mapper.Map< List<CategoryModel>> (createCategoryDtoInList);
            await categoryService.AddCategories(models);
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
