﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace JonkerBudget.WebApi.Controllers.Base
{
    public class NoContent : IHttpActionResult
    {
        private readonly HttpStatusCode _statusCode;        

        public NoContent()
        {
            _statusCode = HttpStatusCode.NoContent;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(string.Empty, 
                                    System.Text.Encoding.UTF8,
                                    "application/json")
            };
            return Task.FromResult(response);
        }
    }
}