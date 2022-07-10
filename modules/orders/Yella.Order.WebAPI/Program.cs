using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Yella.Context;
using Yella.Context.IoC.DependencyResolvers;
using Yella.EntityFrameworkCore.IoC.DependencyResolvers;
using Yella.Order.Application.IoC.DependencyResolvers;
using Yella.Order.Application.Modules.Orders.Mappers;
using Yella.Order.Context.EntityFrameworkCore;
using Yella.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connection = builder.Configuration["ConnectionStrings:SqlServer"];
builder.Services.AddDbContext<YellaDbContext>(x => x.UseSqlServer(connection));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new YellaAutofacBusinessModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacEntityFrameworkCoreModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacContextModule<YellaDbContext>()));

var app = builder.Build();

ServiceActivator.Configure(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
