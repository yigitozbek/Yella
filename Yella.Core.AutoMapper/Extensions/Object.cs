using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yella.Core.Extension;

namespace Yella.Core.AutoMapper.Extensions;

public static class Object
{
    public static List<TKey> ToMapper<TKey>(this object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<List<TKey>>(obj);
    }

    public static List<TKey> ToMapperFirst<TKey>(this object obj)
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        var mapper = ServiceActivator.GetScope()?.ServiceProvider.GetService<IMapper>();
        return mapper!.Map<List<TKey>>(obj);
    }

}