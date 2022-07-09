global using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Yella.Dapper;
using Yella.Dapper.Test;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IDapperRepository, DapperRepository>()
    .BuildServiceProvider();

const string connectionString = "Data Source=.;Initial Catalog=x;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

var dapper = serviceProvider.GetService(typeof(IDapperRepository)) as IDapperRepository;

Debug.Assert(dapper != null, nameof(dapper) + " != null");

var result = dapper.Connection(connectionString);

Console.WriteLine(result?.Message);

var query = new MSDapperTest(dapper).GetPerson().Result;

foreach (var country in query)
{
    Console.WriteLine($"{country.Name} {country.Code}");
}
