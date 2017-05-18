using System.Collections.Generic;
using DragonFire.Core.Auditing;
using DragonFire.Core.Request;
using JonkerBudget.Domain.Repositories;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using JonkerBudget.Domain.Services.Base;
using JonkerBudget.Domain.Models;
using JonkerBudget.Domain.Models.Users;
using System;
using DragonFire.Core.EntityFramework.Uow;

namespace JonkerBudget.Application.Users
{
    [Audit]
    public class UsersService : DomainService, IUsersService
    {
        IUserRepository userRepository;
        IMapper mapper;
        IUnitOfWork unitOfWork;
        IRequestInfoProvider requestInfoProvider;

        public UsersService(IUserRepository userRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            IRequestInfoProvider requestInfoProvider) : base(requestInfoProvider)
        {
            this.requestInfoProvider = requestInfoProvider;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(string Id)
        {
            try
            {
                var userToDelete = await userRepository.GetUser(Id);

                if(userToDelete!= null)
                {
                    userToDelete.Email = "del_" + userToDelete.Email + "_" + Guid.NewGuid().ToString();
                    userToDelete.UserName = "del_" + userToDelete.UserName + "_" + Guid.NewGuid().ToString();
                    userToDelete.LockoutEnabled = true;

                    await unitOfWork.SaveChangesAsync();

                    return true;
                }else
                {
                    return false;
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<UserModel>>(await Task.FromResult(userRepository.GetAll().ToList()));
        }

        public virtual async Task<UserModel> GetUserAsync(string Id)
        {
            return mapper.Map<UserModel>(await userRepository.GetUser(Id));
        }

        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return mapper.Map<IEnumerable<UserModel>>(await userRepository.GetUsers());
        }

        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            var userToUpdate = await userRepository.GetUser(user.Id);

            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.Surname = user.Surname;
            userToUpdate.UserName = user.Username;
            userToUpdate.IsAdUser = user.IsAdUser;

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<UserModel>(userToUpdate);
        }

        public virtual async Task<UserModel> GetAdUserAsync(string userName)
        {
            return mapper.Map<UserModel>(await userRepository.GetAdUser(userName));
        }

        public virtual async Task<bool> UpdateUserAsync(string playerId)
        {
            var userToUpdate = await userRepository.GetUser(requestInfoProvider.Current.UserId);
            userToUpdate.PlayerId = playerId;

            await unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}