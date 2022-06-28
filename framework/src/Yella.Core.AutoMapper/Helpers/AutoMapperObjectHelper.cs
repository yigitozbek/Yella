#nullable enable
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yella.Framework.Extension;

namespace Yella.Framework.AutoMapper.Helpers;

public static class AutoMapperObjectHelper
{
    public static List<TKey> ToMapper<TKey>(this object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<List<TKey>>(obj);
    }

    public static TKey ToMapper<TKey>(this List<object>? obj) 
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));

        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<TKey>(obj);
    }
}