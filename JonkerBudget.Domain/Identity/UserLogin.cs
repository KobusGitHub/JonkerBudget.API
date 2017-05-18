using Microsoft.AspNet.Identity.EntityFramework;

namespace JonkerBudget.Domain.Identity
{
    public class UserLogin : IdentityUserLogin
    {
        public int Id { get; set; }
    }
}
