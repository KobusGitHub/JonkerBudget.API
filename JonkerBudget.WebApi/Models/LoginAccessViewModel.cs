using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGNotify.WebApi.Models
{
    public class LoginAccessViewModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }
    }
}