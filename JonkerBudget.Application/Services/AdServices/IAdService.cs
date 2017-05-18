using Microsoft.Owin.Security.OAuth;
using JonkerBudget.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Services.AdServices
{
    public interface IAdService
    {
        bool ValidateCredentials(string userName, string password, OAuthAuthorizationServerOptions OAuthOptions, out ClaimsIdentity identity);
        IEnumerable<UserDto> GetUserDetails(string inputName);
    }
}
