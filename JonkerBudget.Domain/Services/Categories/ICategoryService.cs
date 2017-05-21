using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.EscalationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategories();
        Task<CategoryModel> AddCategory(CategoryModel categoryModel);
        Task AddCategories(List<CategoryModel> categoryModels);

    }
}
