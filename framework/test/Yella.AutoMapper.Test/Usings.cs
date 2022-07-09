global using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Yella.AutoMapper.Test;
using Yella.AutoMapper.Test.Profiles;
using Yella.Utilities.Extensions;


var serviceProvider = new ServiceCollection()
    .AddAutoMapper(typeof(YellaAutoMapperProfile).Assembly)
    .AddSingleton<Stopwatch>()
    .BuildServiceProvider();

ServiceActivator.Configure(serviceProvider);
var person = new MSAutoMapperTest().GetPerson();
Console.WriteLine(person.FullName);


