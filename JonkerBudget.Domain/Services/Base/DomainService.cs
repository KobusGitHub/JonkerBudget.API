using DragonFire.Core.Domain;
using DragonFire.Core.Request;

namespace JonkerBudget.Domain.Services.Base
{
    public class DomainService : IDomainService
    {
        public DomainService(IRequestInfoProvider requestInfoProvider)
        {
            RequestInfoProvider = requestInfoProvider;
        }

        public IRequestInfoProvider RequestInfoProvider { get; set; }
    }
}
