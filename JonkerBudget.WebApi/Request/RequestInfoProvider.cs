using DragonFire.Core.Request;

namespace JonkerBudget.WebApi.Request
{
    public class RequestInfoProvider : IRequestInfoProvider
    {
        public IRequestInfo Current { get; set; }

        public RequestInfoProvider(IRequestInfo unitOfWork)
        {
            Current = unitOfWork;
        }
    }
}
