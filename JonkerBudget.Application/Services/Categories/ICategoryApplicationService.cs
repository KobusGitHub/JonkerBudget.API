using JonkerBudget.Application.Dto.Categories.Dto.In;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.TaskNotifications
{
    public interface ICategoryApplicationService
    {
        Task<IEnumerable<CategoryDtoOut>> GetAllCategories();
        Task<CategoryDtoOut> AddCategory(CreateCategoryDtoIn createCategoryDtoIn);
    }
}
