using AutoMapper;
using JonkerBudget.Domain.Entities;
using JonkerBudget.Domain.Identity;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Models.Statuses;
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
            CreateMap<NotificationTask, NotificationTaskModel>().ReverseMap();
            CreateMap<NotificationTask, NotificationTaskModelOut>().ReverseMap();
            CreateMap<NotificationTaskUpdate, NotificationTaskUpdateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserManager, UserManagerModel>().ReverseMap();
            CreateMap<Status, StatusModel>().ReverseMap();
            CreateMap<EscalationDetail, EscalationDetailModel>().ReverseMap();
            CreateMap<EscalationDetail, EscalationDetailModelOut>().ReverseMap();
        }
    }
}