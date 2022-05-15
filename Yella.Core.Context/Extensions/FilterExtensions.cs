using System;
using System.Linq;
using System.Reflection;
using Archseptia.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Archseptia.Core.Context.Extensions
{
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
}
