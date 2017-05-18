using System;
using DragonFire.Core.Application;
using DragonFire.Core.Request;

namespace JonkerBudget.Application.Services.Base
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationService(IRequestInfoProvider requestInfoProvider)
        {
            RequestInfoProvider = requestInfoProvider;
        }

        public IRequestInfoProvider RequestInfoProvider { get; set; }        
    }
}
