using Autofac;
using Yella.Core.Identity.Entities;
using Yella.Core.Identity.Helpers.Security.Hashing;
using Yella.Core.Identity.Helpers.Security.JWT;
using Yella.Core.Identity.Interfaces;
using Yella.Core.Identity.Services;

namespace Yella.Core.Identity.IoC.DependencyResolvers;

public class AutofacIdentityModule<TUser, TRole> : Module
    where TUser : IdentityUser<TUser, TRole>, new()
    where TRole : IdentityRole<TUser, TRole>, new()

{

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
        builder.RegisterType<JwtHelper<TUser, TRole>>().As<ITokenHelper<TUser, TRole>>();

        builder.RegisterType<AuthService<TUser, TRole>>().As<IAuthService<TUser, TRole>>();
        builder.RegisterType<UserService<TUser, TRole>>().As<IUserService<TUser, TRole>>();
        builder.RegisterType<RoleService<TUser, TRole>>().As<IRoleService<TUser, TRole>>();
        builder.RegisterType<PermissionService<TUser, TRole>>().As<IPermissionService<TUser, TRole>>();
    }
}