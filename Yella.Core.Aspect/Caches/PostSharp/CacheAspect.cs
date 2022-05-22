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

    }
}