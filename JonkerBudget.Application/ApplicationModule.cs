using Autofac;
using AutoMapper;
using DragonFire.Core.Logging;
using JonkerBudget.Application.Logging;
using System.Collections.Generic;

namespace JonkerBudget.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register all profile classes in the calling assembly
            builder.RegisterAssemblyTypes(typeof(ApplicationMappingProfile).Assembly).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {                
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();
        }
    }
}