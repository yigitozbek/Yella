using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Yella.Context.Extensions;

public static class YellaContextExtension
{
    public static void AddContextService<TApplicationDbContext>(this IServiceCollection services,
        IConfiguration configuration,string connectionString) where TApplicationDbContext : DbContext
    {
        services.AddDbContext<TApplicationDbContext>(option => option.UseSqlServer(connectionString));
    }
}