using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JonkerBudget.WebApi.Startup))]

namespace JonkerBudget.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureAutoFac(app);
        }
    }
}
