using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using DragonFire.Core.Domain;
using DragonFire.Core.EnityFramework;
using DragonFire.Core.EntityFramework;
using DragonFire.Core.EntityFramework.Providers;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Logging;
using DragonFire.Core.Repository;
using DragonFire.Core.Request;
using JonkerBudget.EntityFramework;
using JonkerBudget.EntityFramework.Base;
using JonkerBudget.EntityFramework.Providers;
using JonkerBudget.EntityFramework.Uow;
using Predict.Api.Request;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using JonkerBudget.Domain;

namespace JonkerBudget.WorkerApi.App_Start
{
    public static class AutofacConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Auto wire the application services
            builder.RegisterAssemblyTypes(assemblies)
                    .AssignableTo<IDomainService>()
                    .AsImplementedInterfaces()
                    .InstancePerRequest();

            //Auto wire up generic repository
            builder.RegisterGeneric(typeof(DataContextRepositoryBase<>))
                       .As(typeof(IRepository<>))
                       .InstancePerRequest();

            // Auto wire up the repositories
            builder.RegisterAssemblyTypes(assemblies)
                    .AssignableTo<IRepository>()
                    .AsImplementedInterfaces()
                    .InstancePerRequest();

            builder.RegisterModule(new DomainModule());

            builder.RegisterType<DbContextProvider<DataContext>>().As<IDbContextProvider<DataContext>>();

            // Wire up the Current Unit Of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<CurrentUnitOfWorkProvider>().As<ICurrentUnitOfWorkProvider>();

            //Wire up the Request stuff
            builder.RegisterType<RequestInfo>().As<IRequestInfo>().InstancePerRequest();
            builder.RegisterType<RequestInfoProvider>().As<IRequestInfoProvider>();

            // AutoMapper
            // Register all the profiles
            builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile)).As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new AppProfile());
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                    .As<IMapper>()
                    .InstancePerLifetimeScope();

            // Wire up the Logging
            builder.RegisterAssemblyTypes(assemblies)
                        .AssignableTo<ILogger>()
                        .AsImplementedInterfaces();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}