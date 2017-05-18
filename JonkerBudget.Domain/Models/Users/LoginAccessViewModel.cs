using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.Users
{
    public class LoginAccessViewModel
    {        
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}
