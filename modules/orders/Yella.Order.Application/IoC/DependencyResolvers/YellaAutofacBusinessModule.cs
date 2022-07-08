using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Yella.Order.Application.Modules.Orders.Services;
using Yella.Order.Data.Orders.Interfaces;

namespace Yella.Order.Application.IoC.DependencyResolvers
{
    public class YellaAutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<OrderItemApplicationService>().As<IOrderItemService>();

        }


    }
}
