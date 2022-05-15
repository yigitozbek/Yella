using Archseptia.Core.EntityFrameworkCore;
using Archseptia.Core.Identity.Domain.Entities;
using Archseptia.Core.Identity.Service.Helpers.Security.Hashing;
using Archseptia.Core.Identity.Service.Helpers.Security.JWT;
using Archseptia.Core.Identity.Service.Interfaces;
using Archseptia.Core.Identity.Service.Services;
using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Core.Identity.Domain.Entities;
using Yella.Core.IdentityService.Interfaces;

namespace Yella.Core.IdentityService.IoC.DependencyResolvers
{
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
}
