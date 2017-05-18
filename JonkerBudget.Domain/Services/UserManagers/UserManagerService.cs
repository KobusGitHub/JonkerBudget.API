using JonkerBudget.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JonkerBudget.Domain.Models.UserManagers;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Repositories;
using AutoMapper;
using DragonFire.Core.EntityFramework.Uow;
using JonkerBudget.Domain.Entities;

namespace JonkerBudget.Domain.Services.UserManagers
{
    public class UserManagerService : DomainService, IUserManagerService
    {
        IUserManagerRepository userManagerRepository;
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public UserManagerService(IRequestInfoProvider requestInfoProvider, IMapper mapper,
            IUnitOfWork unitOfWork, IUserManagerRepository userManagerRepository) : base(requestInfoProvider)
        {
            this.userManagerRepository = userManagerRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task DeleteUserManagerAsync(int Id)
        {
            userManagerRepository.Delete(Id);
            await unitOfWork.SaveChangesAsync();
        }

        public virtual async Task<UserManagerModel> GetUserManagerAsync(int Id)
        {
            return mapper.Map<UserManagerModel>(await userManagerRepository.GetUserManager(Id));
        }

        public virtual async Task<IEnumerable<UserManagerModel>> GetUserManagersAsync()
        {
            return mapper.Map<IEnumerable<UserManagerModel>>(await userManagerRepository.GetAllListAsync());
        }

        public virtual async Task<UserManagerModel> InsertUserManagerAsync(UserManagerModel userManager)
        {
            var newUserManager = await userManagerRepository.InsertUserManager(mapper.Map<UserManager>(userManager));
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<UserManagerModel>(newUserManager);
        }

        public virtual async Task<UserManagerModel> UpdateUserManagerAsync(UserManagerModel userManager)
        {
            var userManagerToUpdate = await userManagerRepository.FirstOrDefaultAsync(userManager.Id);

            userManagerToUpdate.DateUpdatedUtc = DateTime.Now;
            userManagerToUpdate.UserId = userManager.UserId;
            userManagerToUpdate.ManagerId = userManager.ManagerId;

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<UserManagerModel>(userManagerToUpdate);
        }
    }
}
