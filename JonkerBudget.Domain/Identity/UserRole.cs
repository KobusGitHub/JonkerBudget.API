using Microsoft.AspNet.Identity.EntityFramework;

namespace JonkerBudget.Domain.Identity
{
    public class UserRole : IdentityUserRole
    {       
        public int Id { get; set; }                      
    }
}
