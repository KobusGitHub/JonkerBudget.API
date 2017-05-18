using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.Users.Dto
{
    public class UserDtoOut
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string FullName { get { return this.FirstName + " " + this.Surname; } }
    }
}
