using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using DragonFire.Core.Logging;
using DragonFire.Core.Request;
using DragonFire.Intercept;
using DragonFire.Module;
using JonkerBudget.Application;
using JonkerBudget.Domain;
using JonkerBudget.Domain.Repositories;
using JonkerBudget.EntityFramework.Repositories;
using JonkerBudget.WebApi.Request;
using Owin;
using System;
using System.Reflection;
using System.Web.Http;
using JonkerBudget.Application.Services.AdServices;
using JonkerBudget.Domain.Services.UserManagers;

namespace JonkerBudget.WebApi
{
    public partial class Startup
    {
        public void ConfigureAutoFac(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Wire up the Request stuff
            builder.RegisterType<RequestInfo>().As<IRequestInfo>().InstancePerRequest();
            builder.RegisterType<RequestInfoProvider>().As<IRequestInfoProvider>();

            builder.RegisterType<AdService>().As<IAdService>().InstancePerRequest();
            builder.RegisterType<UserManagerService>().As<IUserManagerService>().InstancePerRequest();

            // Logging
            builder.RegisterType<ILogger>().AsSelf().InstancePerRequest();

            // Register the dragon fire core module
            builder.RegisterModule(new DragonFireModule());

            // Register module for different layers
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new DomainModule());

            // AutoMapper
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            // Explicit repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<UserManagerRepository>().As<IUserManagerRepository>().InstancePerRequest();

            // Register method interceptor module
            builder.RegisterModule(new InterceptModule());

            // Build and Resolve
            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            app.UseAutofacWebApi(GlobalConfiguration.Configuration);
        }
    }    
}