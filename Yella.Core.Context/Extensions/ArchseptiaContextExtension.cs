using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Yella.Framework.Context.Extensions;

public static class ArchseptiaContextExtension
{
    public static void AddContextService<TApplicationDbContext>(this IServiceCollection services,
        IConfiguration configuration,string connectionString) where TApplicationDbContext : DbContext
    {
        services.AddDbContext<TApplicationDbContext>(option => option.UseSqlServer(connectionString));
    }
}