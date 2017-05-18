using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

namespace SGNotify.WebApi.Identity
{
    public class LocalUserLoginProvider : ILoginProvider
    {
        public bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity)
        {
            // SUPERGRP - valdidate domain from username entered against the supergrp.net domain...
            using (var pc = new PrincipalContext(ContextType.Machine))
            {
                bool isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                }
                else
                {
                    identity = null;
                }
                return isValid;
            }
        }
    }
}