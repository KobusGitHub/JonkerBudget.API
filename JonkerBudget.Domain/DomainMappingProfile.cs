using AutoMapper;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Identity;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.Expenses;
using JonkerBudget.Domain.Models.UserManagers;
using JonkerBudget.Domain.Models.Users;

namespace JonkerBudget.Domain
{
    public class DomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            //CreateMap<Order, OrderModel>().ReverseMap();
            //CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserManager, UserManagerModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Expense, ExpenseModel>().ReverseMap();

        }
    }
}