using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Yella.Utilities.Extensions;
using Yella.Utilities.Interceptors;

namespace Yella.Aspect.Autofac.Performances
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = (Stopwatch)ServiceActivator.GetScope()?.ServiceProvider.GetService(typeof(Stopwatch))!;
        }

        protected override void OnBefore(IInvocation invocation) => _stopwatch.Start();

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType?.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }

    }
}
