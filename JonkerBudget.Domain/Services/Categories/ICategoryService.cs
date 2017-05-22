using JonkerBudget.Domain.Models.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task<CategoryModel> AddCategory(CategoryModel categoryModel);
        Task<CategoryModel> UpdateCategory(CategoryModel categoryModel);
        Task AddCategories(List<CategoryModel> categoryModels);

    }
}
