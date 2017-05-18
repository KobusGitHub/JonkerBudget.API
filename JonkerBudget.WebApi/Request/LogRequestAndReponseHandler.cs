using NLog;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JonkerBudget.WebApi.Request
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        private static readonly Logger logger = LogManager.GetLogger("log");

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await LogRequestDetails(request);

            //let other handlers process the request
            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    //Log responses here
                    //var responseBody = task.Result.Content.ReadAsStringAsync().Result;
                    //Trace.WriteLine(responseBody);

                    return task.Result;
                });
        }

        private async Task LogRequestDetails(HttpRequestMessage request)
        {
            var contentType = request.Content.Headers.ContentType;
            var body = string.Empty;

            if (contentType != null && contentType.MediaType != null && contentType.MediaType == "multipart/form-data")
                body = "multipart/form-data";
            else
                body = await request.Content.ReadAsStringAsync();

            if (ExcludeContent(request.Method.ToString(), request.RequestUri.ToString()))
                body = "excluded";

            var dict = new Dictionary<string, string>();

            dict.Add("Method", request.Method.ToString());
            dict.Add("Url", request.RequestUri.ToString());
            dict.Add("Body", body);
            dict.Add("Username", GetUserName());

            string entry = string.Empty;

            foreach (var key in dict)
            {
                entry += string.Format("({0}:{1})", key.Key, key.Value);
            }

            logger.Info(entry);
        }

        private static bool ExcludeContent(string method, string url)
        {
            //Add custom exclusions here
            return false;
        }

        private static string GetUserName()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(userName)) return "Anonymous";
            return userName;
        }
    }
}