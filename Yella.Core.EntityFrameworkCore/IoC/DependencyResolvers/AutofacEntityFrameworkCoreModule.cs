using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yella.Core.EntityFrameworkCore.IoC.DependencyResolvers
{
    public class AutofacEntityFrameworkCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfCoreGenericRepository<,>)).As(typeof(IRepository<,>));
            builder.RegisterGeneric(typeof(EfCoreGenericRepository<>)).As(typeof(IRepository<>));
        }
    }
}
