using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yella.Core.Context;
using Yella.Core.Identity.Entities;

namespace Yella.Core.Identity.Context;

public interface IIdentityDbContext<TUser, TRole>
    where TUser : IdentityUser<TUser, TRole>, new()
    where TRole : IdentityRole<TUser, TRole>, new()

{
    public DbSet<TUser> Users { get; set; }
    public DbSet<TRole> Roles { get; set; }
    public DbSet<UserRole<TUser, TRole>> UserRoles { get; set; }
    public DbSet<Permission<TUser, TRole>> Permissions { get; set; }
    public DbSet<PermissionRole<TUser, TRole>> PermissionRoles { get; set; }
    public DbSet<UserLoginLog<TUser, TRole>> UserLoginLogs { get; set; }
}