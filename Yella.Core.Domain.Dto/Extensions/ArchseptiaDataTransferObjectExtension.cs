﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Archseptia.Core.Domain.Dto.Extensions
{
    public static class ArchseptiaDataTransferObjectExtension
    {
        public static void AddDataTransferObjectService(this IServiceCollection services, params Assembly[] assemblies) => services.AddAutoMapper(assemblies);
    }
}
