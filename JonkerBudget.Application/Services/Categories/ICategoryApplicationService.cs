using JonkerBudget.Application.Dto.Categories.Dto.In;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public interface ICategoryApplicationService
    {
        Task<IEnumerable<CategoryDtoOut>> GetAllCategories();
        Task<CategoryDtoOut> AddCategory(CreateCategoryDtoIn createCategoryDtoIn);
        Task<CategoryDtoOut> UpdateCategory(CreateCategoryDtoIn createCategoryDtoIn);
        Task AddCategories(List<CreateCategoryDtoIn> createCategoryDtoInList);

    }
}
