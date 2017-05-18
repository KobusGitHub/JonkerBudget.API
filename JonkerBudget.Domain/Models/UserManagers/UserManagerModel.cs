using JonkerBudget.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Domain.Models.UserManagers
{
    public class UserManagerModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public string ManagerId { get; set; }
        public UserModel Manager { get; set; }
    }
}
