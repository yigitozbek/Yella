using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Yella.Core.Context.Extensions
{
    public static class ArchseptiaContextExtension
    {
        public static void AddContextService<TApplicationDbContext>(this IServiceCollection services,
            IConfiguration configuration) where TApplicationDbContext : DbContext
        {
            var connection = configuration["ConnectionStrings:SqlServer"];
            services.AddDbContext<TApplicationDbContext>(option => option.UseSqlServer(connection));
        }
    }
}
