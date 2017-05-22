using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public async Task<CategoryModel> UpdateCategory(CategoryModel categoryModel)
        {
            var categories = await this.categoryRepository.GetAll().ToListAsync();

            var category = categories.FirstOrDefault(x => x.GuidId == categoryModel.GuidId);

            category.Budget = categoryModel.Budget;
            category.CategoryName = categoryModel.CategoryName;
            category.DateUpdatedUtc = DateTime.Now;

            await unitOfWork.SaveChangesAsync();
            return mapper.Map<CategoryModel>(category);
        }

        public async Task<CategoryModel> AddCategory(CategoryModel categoryModel)
        {
            var category = await this.categoryRepository.InsertAsync(mapper.Map<Category>(categoryModel));
            await unitOfWork.SaveChangesAsync();

            var returnModel = mapper.Map<CategoryModel>(category);

            return returnModel;
        }

        public async Task AddCategories(List<CategoryModel> categoryModels)
        {
            var categories = await this.categoryRepository.GetAll().ToListAsync();


            foreach (var categoryModel in categoryModels)
            {
                var category = categories.FirstOrDefault(x => x.GuidId == categoryModel.GuidId);
                if (category != null)
                {
                    category.Budget = categoryModel.Budget;
                    category.CategoryName = categoryModel.CategoryName;
                    category.DateUpdatedUtc = DateTime.Now;
                }
                else
                {
                    await this.categoryRepository.InsertAsync(mapper.Map<Category>(categoryModel));
                }
            }
            await unitOfWork.SaveChangesAsync();
        }
    }
}
