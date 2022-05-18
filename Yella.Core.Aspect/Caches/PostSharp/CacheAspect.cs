using System.Security.Principal;
using PostSharp.Aspects;
using PostSharp.Serialization;
using Yella.Core.CrossCuttingConcern.Cache.Microsoft;

namespace Yella.Core.Aspect.Caches.PostSharp;

[PSerializable]
public class CacheAspect : MethodInterceptionAspect
{
    public int ExpirationPeriod = 30;
    public override void OnInvoke(MethodInterceptionArgs args)
    {
        var cache = MethodResultCache.GetCache(args.Method);
        cache.ExpirationPeriod = ExpirationPeriod;
        var arguments = args.Arguments.Union(new[] { WindowsIdentity.GetCurrent().Name }).ToList();
        var result = cache.GetCachedResult(arguments);
        if (result != null)
        {
            args.ReturnValue = result;
            return;
        }

        base.OnInvoke(args);
        cache.CacheCallResult(args.ReturnValue, arguments);
    }
}