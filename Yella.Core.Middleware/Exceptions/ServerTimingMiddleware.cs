using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Yella.Core.Middleware.Exceptions
{
    public class ServerTimingMiddleware : IMiddleware
    {
        private const string ServerTimingHttpHeader = "Server-Timing";

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(next);

            if (context.Response.SupportsTrailers())
            {
                context.Response.DeclareTrailer(ServerTimingHttpHeader);
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                await next(context).ConfigureAwait(false);

                stopWatch.Stop();
                context.Response.AppendTrailer(ServerTimingHttpHeader, $"app;dur={stopWatch.ElapsedMilliseconds}.0");
            }
            else
            {
                await next(context).ConfigureAwait(false);
            }
        }
    }

}
