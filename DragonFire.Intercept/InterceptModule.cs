using Autofac;
using Autofac.Extras.DynamicProxy2;
using DragonFire.Core.Application;
using DragonFire.Core.Domain;
using DragonFireTemplate.Intercept;
using System;

namespace DragonFire.Intercept
{
    public class InterceptModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();            

            // Application layer interceptor - remove if not needed
            builder.RegisterType<ApplicationServiceMethodInterceptor>().SingleInstance();
            
            // Domain layer interceptor - remove if not needed
            builder.RegisterType<DomainServiceMethodInterceptor>().SingleInstance();

            // Auto wire the application services
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IApplicationService>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(ApplicationServiceMethodInterceptor));

            // Auto wire the application services
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerRequest()
                .EnableClassInterceptors()
                .InterceptedBy(typeof(DomainServiceMethodInterceptor));
        }
    }
}
