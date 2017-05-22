using AutoMapper;
using JonkerBudget.Application.Dto.Categories.Dto.In;
using JonkerBudget.Application.Dto.Categories.Dto.Out;
using JonkerBudget.Application.Dto.EscalationDetails.Dto.Out;
using JonkerBudget.Application.Dto.Expenses.Dto.In;
using JonkerBudget.Application.Dto.Expenses.Dto.Out;
using JonkerBudget.Application.Dto.UserManagers.Dto;
using JonkerBudget.Application.Dto.Users.Dto;
using JonkerBudget.Application.Users.Dto;
using JonkerBudget.Domain.Models;
using JonkerBudget.Domain.Models.Categories;
using JonkerBudget.Domain.Models.Expenses;
using JonkerBudget.Domain.Models.UserManagers;
using JonkerBudget.Domain.Models.Users;

namespace JonkerBudget.Application
{
    public class ApplicationMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserDtoOut>().ReverseMap();
            CreateMap<UserManagerModel, UserManagerDto>().ReverseMap();
            
            CreateMap<CategoryModel, CreateCategoryDtoIn>().ReverseMap();
            CreateMap<CategoryModel, CategoryDtoOut>().ReverseMap();
            CreateMap<ExpenseModel, CreateExpenseDtoIn>().ReverseMap();
            CreateMap<ExpenseModel, ExpenseDtoOut>().ReverseMap();



        }
    }    
}
