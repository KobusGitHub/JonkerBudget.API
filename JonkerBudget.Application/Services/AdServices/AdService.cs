using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using JonkerBudget.Application.OAuthProvider;
using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using JonkerBudget.Application.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.DirectoryServices;

namespace JonkerBudget.Application.Services.AdServices
{
    public class AdService : IAdService
    {

        public bool ValidateCredentials(string userName, string password, OAuthAuthorizationServerOptions OAuthOptions, out ClaimsIdentity identity)
        {
            
            using (var pc = new PrincipalContext(ContextType.Machine))
            {
                bool isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    identity = new ClaimsIdentity(OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                }
                else
                {
                    identity = null;
                }
                return isValid;
            }
        }
       
        public IEnumerable<UserDto> GetUserDetails(string inputName)
        {
            if (inputName == null || inputName.Trim().Length == 0)
                yield return null;
            
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                using (var PSearcher = new PrincipalSearcher(new UserPrincipal(ctx)))
                {
                    foreach (UserPrincipal user in PSearcher.FindAll())
                    {
                        if(Regex.IsMatch(user.SamAccountName.ToLower(), inputName.ToLower())
                            && user.SamAccountName.ToLower().Split('.').Length > 1)
                        {
                            string[] fullName = user.SamAccountName.ToLower().Split('.');

                            yield return new UserDto
                            {
                                FirstName = fullName[0],
                                Surname = fullName[1],
                                Email = user.EmailAddress,
                                Username = user.SamAccountName,
                                IsAdUser = true
                            };
                        }
                    }
                }
            }
        }

    }




}
