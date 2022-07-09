using Autofac;
using Microsoft.EntityFrameworkCore;

namespace Yella.Context.IoC.DependencyResolvers;

public class AutofacContextModule<TContext> : Module
    where TContext : DbContext
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CoreDbContext<TContext>>().As<IApplicationDbContext>();
    }
}