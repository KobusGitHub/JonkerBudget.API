using AutoMapper;
using JonkerBudget.Application.Dto.EscalationDetails.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTasks.Dto.Out;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.In;
using JonkerBudget.Application.Dto.NotificationTaskUpdates.Dto.Out;
using JonkerBudget.Application.Dto.UserManagers.Dto;
using JonkerBudget.Application.Dto.Users.Dto;
using JonkerBudget.Application.Users.Dto;
using JonkerBudget.Domain.Models;
using JonkerBudget.Domain.Models.EscalationDetails;
using JonkerBudget.Domain.Models.NotificationTasks;
using JonkerBudget.Domain.Models.NotificationTaskUpdates;
using JonkerBudget.Domain.Models.UserManagers;
using JonkerBudget.Domain.Models.Users;

namespace JonkerBudget.Application
{
    public class ApplicationMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<NotificationTaskUpdateModel, NotificationTaskUpdateDtoOut>().ReverseMap();
            CreateMap<NotificationTaskModel, NotificationTaskDtoOut>().ReverseMap();
            CreateMap<NotificationTaskModelOut, NotificationTaskDtoOut>().ReverseMap();
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, UserDtoOut>().ReverseMap();
            CreateMap<UserManagerModel, UserManagerDto>().ReverseMap();
            CreateMap<EscalationDetailModel, EscalationDetailDtoOut>().ReverseMap();
            
            CreateMap<NotificationTaskModel, NotificationTaskDtoIn>().ReverseMap();
            CreateMap<NotificationTaskModelOut, NotificationTaskDtoIn>().ReverseMap();
            
            CreateMap<CreateNotificationTaskUpdateModel, NotificationTaskUpdateDtoIn>().ReverseMap();
            CreateMap<CreateNotificationTaskStatusUpdateModel, NotificationTaskStatusUpdateDtoIn>().ReverseMap();
            
        }
    }    
}
