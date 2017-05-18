using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SGNotify.WebApi.Identity
{
    public interface ILoginProvider
    {
        bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity);
    }
}