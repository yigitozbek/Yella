using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yella.Utilities.Extensions;

namespace Yella.AutoMapper.Extensions;

public static class YellaAutoMapperObjectExtension
{
    public static List<TEntity> ToMapper<TEntity>(this List<object>? obj) where TEntity : class
    {
        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<List<TEntity>>(obj);
    }

    public static TEntity ToMapper<TEntity>(this object obj) where TEntity : class
    {
        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<TEntity>(obj);
    }
}