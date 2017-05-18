using Microsoft.AspNet.Identity.EntityFramework;

namespace JonkerBudget.Domain.Identity
{
    public class Role 
        : IdentityRole<string, UserRole>
    {
    }
}