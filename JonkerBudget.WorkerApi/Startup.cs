using JonkerBudget.WorkerApi.App_Start;
using Owin;
using System.Web.Http;

namespace JonkerBudget.WorkerApi
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var configuration = new HttpConfiguration();

            WebApiConfig.Register(configuration);
            AutofacConfig.Configure(configuration);

            appBuilder.UseWebApi(configuration);
        }
    }
}
