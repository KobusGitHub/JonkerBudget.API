using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace JonkerBudget.WebApi.Global
{
    public class CustomErrorResult : IHttpActionResult
    {
        private HttpStatusCode errorCode = HttpStatusCode.InternalServerError;
        private bool specificErrorCode = false;
        public CustomErrorResult(HttpStatusCode errorCode)
        {
            this.errorCode = errorCode;
            specificErrorCode = true;
        }

        public CustomErrorResult()
        {
        }

        public HttpRequestMessage Request { get; set; }

        public string Content { get; set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;

            if (!specificErrorCode)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            response.Content = new StringContent(Content);
            response.RequestMessage = Request;
            return Task.FromResult(response);
        }
    }
}