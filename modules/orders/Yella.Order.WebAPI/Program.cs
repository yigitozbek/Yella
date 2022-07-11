using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Yella.Context;
using Yella.Context.IoC.DependencyResolvers;
using Yella.EntityFrameworkCore.IoC.DependencyResolvers;
using Yella.Identity.Helpers.Security.JWT;
using Yella.Identity.IoC.DependencyResolvers;
using Yella.Order.Application.IoC.DependencyResolvers;
using Yella.Order.Application.Modules.Orders.Mappers;
using Yella.Order.Context.EntityFrameworkCore;
using Yella.Order.Domain.Identities;
using Yella.Order.WebAPI.Controllers;
using Yella.Utilities.Extensions;
using Yella.WebAPI.Helpers.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

var contact = new OpenApiContact()
{
    Name = "",
    Email = "",
    Url = new Uri("")
};

var license = new OpenApiLicense()
{
    Name = "Free License",
    Url = new Uri("")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "",
    Description = "",
    TermsOfService = new Uri(""),
    Contact = contact,
    License = license
};

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
});


builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new CamelCaseParameterTransformer()));
    options.EnableEndpointRouting = false;
});

builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connection = builder.Configuration["ConnectionStrings:SqlServer"];
builder.Services.AddDbContext<YellaDbContext>(x => x.UseSqlServer(connection));

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<JwtHelper<User, Role>.TokenOptions>();
var sessionOption = builder.Configuration.GetSection("SessionOption").Get<JwtHelper<User, Role>.SessionOption>();

builder.Services.AddAuthentication(option =>
    {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = false,
            ValidateAudience = true,
            ValidAudience = tokenOptions.Audience,
            ValidIssuer = tokenOptions.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
            ValidateIssuer = true,
        };
    });


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new YellaAutofacBusinessModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacEntityFrameworkCoreModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacContextModule<YellaDbContext>()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacIdentityModule<User,Role>()));

var app = builder.Build();

ServiceActivator.Configure(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
