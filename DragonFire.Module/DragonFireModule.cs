using Autofac;
using DragonFire.Core.Application;
using DragonFire.Core.Domain;
using DragonFire.Core.EnityFramework;
using DragonFire.Core.EntityFramework;
using DragonFire.Core.EntityFramework.Providers;
using DragonFire.Core.EntityFramework.Uow;
using DragonFire.Core.Logging;
using DragonFire.Core.Repository;
using JonkerBudget.EntityFramework;
using JonkerBudget.EntityFramework.Base;
using JonkerBudget.EntityFramework.Providers;
using JonkerBudget.EntityFramework.Uow;
using System;

namespace DragonFire.Module
{
    public class DragonFireModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                 .AssignableTo<IApplicationService>()
                 .AsImplementedInterfaces()
                 .InstancePerRequest();

            // Auto wire the application services
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IDomainService>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            // Auto wire up the repositories
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IRepository>()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            //Auto wire up generic repository
            builder.RegisterGeneric(typeof(DataContextRepositoryBase<>))
                   .As(typeof(IRepository<>))
                   .InstancePerRequest();

            //Auto wire up generic repository
            builder.RegisterGeneric(typeof(DataContextRepositoryBase<,>))
                       .As(typeof(IRepository<,>))
                       .InstancePerRequest();

            builder.RegisterType<DbContextProvider<DataContext>>().As<IDbContextProvider<DataContext>>();

            // Wire up the Current Unit Of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<CurrentUnitOfWorkProvider>().As<ICurrentUnitOfWorkProvider>();

            // Wire up the Logging
            builder.RegisterAssemblyTypes(assemblies)
                    .AssignableTo<ILogger>()
                    .AsImplementedInterfaces();
        }
    }
}
