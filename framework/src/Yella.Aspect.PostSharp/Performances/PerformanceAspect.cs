using System.Diagnostics;
using System.Transactions;
using Castle.DynamicProxy;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using PostSharp.Serialization;
using Yella.Utilities.Extensions;
using Yella.Utilities.Interceptors;

namespace Yella.Aspect.PostSharp.Performances;

[PSerializable]
[AttributeUsage(AttributeTargets.Method)]
[ProvideAspectRole(StandardRoles.PerformanceInstrumentation)]
public class PerformanceAspect : OnMethodBoundaryAspect
{
    private int _interval;

    public PerformanceAspect(int interval)
    {
        _interval = interval;
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        ((Stopwatch)ServiceActivator.GetScope()?.ServiceProvider.GetService(typeof(Stopwatch))!)?.Start();
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        var stopwatch = (Stopwatch)ServiceActivator.GetScope()?.ServiceProvider.GetService(typeof(Stopwatch))!;

        if (stopwatch.Elapsed.TotalSeconds > _interval)
        {
            Debug.WriteLine($"Performance : {args.Method.DeclaringType?.FullName}.{args.Method.Name}-->{stopwatch.Elapsed.TotalSeconds}");
        }

        stopwatch.Reset();
    }



}