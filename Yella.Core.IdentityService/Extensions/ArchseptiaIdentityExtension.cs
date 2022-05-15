using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Yella.Core.Identity.Domain.Entities;
using Yella.Core.IdentityService.Helpers.Security.Encryption;
using Yella.Core.IdentityService.Helpers.Security.JWT;
using Yella.Core.IdentityService.Middlewares;

namespace Yella.Core.IdentityService.Extensions
{
    public static class ArchseptiaIdentityExtension
    {
        public static void AddIdentityService<TUser, TRole>(this IServiceCollection services,
            IConfiguration configuration)
            where TUser : IdentityUser<TUser, TRole>
            where TRole : IdentityRole<TUser, TRole>
        {

            var tokenOptions = configuration.GetSection("TokenOptions").Get<JwtHelper<TUser, TRole>.TokenOptions>();
            var sessionOption = configuration.GetSection("SessionOption").Get<JwtHelper<TUser, TRole>.SessionOption>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionOption.IdleTimeout);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = sessionOption.CookieName;
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = tokenOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions.Issuer,
                };
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

        }


        public static void AddIdentityConfigure(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();

            app.UseSession();

            app.UseIdentity();

            app.UseAuthentication();

            app.UseAuthorization();
        }
    }


}
