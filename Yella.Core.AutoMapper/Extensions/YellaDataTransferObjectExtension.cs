using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Yella.Framework.AutoMapper.Extensions;

public static class YellaDataTransferObjectExtension
{
    public static void AddDataTransferObjectService(this IServiceCollection services, params Assembly[] assemblies) => services.AddAutoMapper(assemblies);
}