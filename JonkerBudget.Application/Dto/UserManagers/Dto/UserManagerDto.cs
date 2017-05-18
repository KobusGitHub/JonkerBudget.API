using JonkerBudget.Application.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonkerBudget.Application.Dto.UserManagers.Dto
{
    public class UserManagerDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
        public string ManagerId { get; set; }
        public UserDto Manager { get; set; }
    }
}
