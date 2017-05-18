using System;
using DragonFire.Core.Request;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading;

namespace JonkerBudget.WebApi.Request
{
    public class RequestInfo : IRequestInfo
    {
        private string requestId = null;
        private string userId = null;

        public string RequestId
        {
            get
            {
                return requestId;
            }

            set
            {
                requestId = value;
            }
        }

        public string Username
        {
            get
            {
                return GetRequestUsername();
            }
        }

        public string UserId
        {
            get
            {
                return GetRequestUserId();
            }
            set
            {
                userId = value;
            }
        }

        private string GetRequestUserId()
        {
            try
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string GetRequestUsername()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(userName)) return "Anonymous";
            return userName;
        }

        public void Dispose()
        {            
        }
    }
}