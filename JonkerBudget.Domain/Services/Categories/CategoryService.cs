using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Services.Base;
using JonkerBudget.Domain.Services.NotificationTasks;
using JonkerBudget.Domain.Services.NotificationTaskUpdates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Services.EscalationDetails
{
    public class CategoryService : DomainService, ICategoryService
    {
        private IMapper mapper;
        private IRepository<Category> categoryRepository;
        private IUnitOfWork unitOfWork;
       
        public CategoryService(IRequestInfoProvider requestInfoProvider, 
            IRepository<Category> categoryRepository,
            IMapper mapper, IUnitOfWork unitOfWork) : base(requestInfoProvider)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        

        public async Task<IEnumerable<CategoryModel>> GetAllCategories()
        {
            var categories = await this.categoryRepository.GetAll().ToListAsync();
            return mapper.Map<List<CategoryModel>>(categories); ;
        }

        public async Task<CategoryModel> AddCategory(CategoryModel categoryModel)
        {
            var category = await this.categoryRepository.InsertAsync(mapper.Map<Category>(categoryModel));
            await unitOfWork.SaveChangesAsync();

            var returnModel = mapper.Map<CategoryModel>(category);

            return returnModel;
        }
    }
}
