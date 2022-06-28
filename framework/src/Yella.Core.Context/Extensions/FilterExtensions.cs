using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Yella.Framework.Domain.Entities;

namespace Yella.Framework.Context.Extensions;

public static class FilterExtensions
{
    public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType) =>
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, new object[] { modelBuilder });

    private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(FilterExtensions)
        .GetMethods(BindingFlags.Public | BindingFlags.Static)
        .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

    public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, IFullAuditedEntity =>
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
}