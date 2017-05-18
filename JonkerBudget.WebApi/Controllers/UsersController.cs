using AutoMapper;
using DragonFire.Core.Request;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using JonkerBudget.Application.Dto.Ad_Users.Dto;
using JonkerBudget.Application.Services.AdServices;
using JonkerBudget.Application.Users;
using JonkerBudget.Application.Users.Dto;
using JonkerBudget.Domain.Models.Users;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace JonkerBudget.WebApi.Controllers
{
    
    [RoutePrefix("api/Users")]
    public class UsersController : ControllerBase
    {                
        IUsersService usersService;
        private readonly IAdService loginProvider;
        IMapper mapper;

        public UsersController(IUsersService usersService, IAdService loginProvider,
            IRequestInfoProvider requestInfoProvider, IMapper mapper) : base(requestInfoProvider)
        {
            this.usersService = usersService;
            this.loginProvider = loginProvider;
            this.mapper = mapper;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAdUsers(string inputName)
        {
            var result = loginProvider.GetUserDetails(inputName);
            return Ok(result);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await usersService.GetUsersAsync();
            return Ok(result);
        }

       // [Authorize(Roles = "Administrator")]
        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string id)
        {
            var result = await usersService.DeleteUserAsync(id);
            return Ok(result);
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await usersService.UpdateUserAsync(mapper.Map<UserModel>(user));

            return Ok(updated);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdatePlayerId")]
        public async Task<IHttpActionResult> UpdatePlayerId(string playerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await usersService.UpdateUserAsync(playerId);

            return Ok(updated);
        }

        //[Authorize]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var result = await usersService.GetUserAsync(Id);
            return Ok(result);
        }

        [HttpPost, Route("Token")]
        public IHttpActionResult Token([FromBody]AdUsers login)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            ClaimsIdentity identity;

            if (!loginProvider.ValidateCredentials(login.UserName, login.Password, Startup.OAuthOptions, out identity))
            {
                return BadRequest("Incorrect user or password");
            }

            var userName = login.UserName;

            //Get Id of the user that logged in
            var strippedUsername = userName.Replace(@"SUPERGRP\", "");

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(131400));

            return Ok(new LoginAccessViewModel
            {
                UserName = login.UserName,
                AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket),
                ExpiryDate = ticket.Properties.ExpiresUtc
            });
        }


    }
}
