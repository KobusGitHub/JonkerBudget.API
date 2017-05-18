using DragonFire.Core.Request;
using JonkerBudget.WebApi.Controllers.Base;
using System;
using System.Web.Http;

namespace JonkerBudget.WebApi.Controllers
{
    public class ControllerBase : ApiController
    {
        public IRequestInfoProvider RequestInfoProvider;

        public ControllerBase(IRequestInfoProvider requestInfoProvider)
        {            
            RequestInfoProvider = requestInfoProvider;

            var requestId = Guid.NewGuid().ToString();

            if (requestInfoProvider.Current != null)
            {
                if (requestInfoProvider.Current.RequestId == null)
                    requestInfoProvider.Current.RequestId = requestId;
            }
        }

        protected IHttpActionResult NoContent()
        {
            return new NoContent();
        }
    }
}