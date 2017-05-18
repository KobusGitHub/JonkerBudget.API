using DragonFire.Core.Request;

namespace Predict.Api.Request
{
    public class RequestInfoProvider : IRequestInfoProvider
    {
        public IRequestInfo Current { get; set; }

        public RequestInfoProvider(IRequestInfo requestInfo)
        {
            Current = requestInfo;
        }
    }
}
