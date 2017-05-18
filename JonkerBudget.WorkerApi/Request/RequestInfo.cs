using System;
using DragonFire.Core.Request;

namespace Predict.Api.Request
{
    public class RequestInfo : IRequestInfo
    {
        private string requestId = string.Empty;
        private string userName = string.Empty;
        private string userId = string.Empty;        

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
                return userName;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public void Dispose()
        {
        }
    }
}
