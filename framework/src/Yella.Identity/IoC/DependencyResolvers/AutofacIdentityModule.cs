using Autofac;
using Yella.Identity.Entities;
using Yella.Identity.Helpers.Security.JWT;
using Yella.Identity.Interfaces;
using Yella.Identity.Services;
using Yella.Utilities.Security.Hashing;

namespace Yella.Identity.IoC.DependencyResolvers;

public class AutofacIdentityModule<TUser, TRole> : Module
    where TUser : IdentityUser<TUser, TRole>, new()
    where TRole : IdentityRole<TUser, TRole>, new()

{

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
        builder.RegisterType<JwtHelper<TUser, TRole>>().As<ITokenHelper<TUser, TRole>>();

        builder.RegisterType<AuthService<TUser, TRole>>().As<IAuthService<TUser, TRole>>();
        builder.RegisterType<IdentityUserService<TUser, TRole>>().As<IIdentityUserService<TUser, TRole>>();
        builder.RegisterType<IdentityRoleService<TUser, TRole>>().As<IIdentityRoleService<TUser, TRole>>();
        builder.RegisterType<IdentityPermissionService<TUser, TRole>>().As<IIdentityPermissionService<TUser, TRole>>();
    }
}