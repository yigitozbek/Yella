using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Yella.Framework.Dapper.IoC.DependencyResolvers;

public class AutofacDapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DapperRepository>().As<IDapperRepository>();
    }

}