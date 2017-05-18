using JonkerBudget.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace JonkerBudget.Domain.Identity
{
    public class User
        : IdentityUser<string, UserLogin, UserRole, UserClaim>
    { 
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool IsAdUser { get; set; }
        public string PlayerId { get; set; }
    }
}
