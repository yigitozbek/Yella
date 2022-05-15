using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Archseptia.Core.CrossCuttingConcern.Cache.Microsoft;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Archseptia.Core.Aspect.Cache.PostSharp
{
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
}
