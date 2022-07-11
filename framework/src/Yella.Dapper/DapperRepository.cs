using System.Data.SqlClient;
using Dapper;
using Yella.Utilities.Results;

namespace Yella.Dapper;

public class DapperRepository : IDapperRepository
{
    public static string? ConnectionString;

    public IResult Connection(string connectionString)
    {

        if (!string.IsNullOrEmpty(ConnectionString))
        {
            return new ErrorResult("Connection String has been defined before.");
        }

        ConnectionString = connectionString;

        return new SuccessResult("Successful");
    }

    public async Task<List<TEntity>> GetListAsync<TEntity>(string command) 
        where TEntity : class
    {
        await using var connection = new SqlConnection(ConnectionString);
        return (await connection.QueryAsync<TEntity>(command)).AsList();
    }

    public async Task<TEntity> GetAsync<TEntity>(string command)
        where TEntity : class
    {
        await using var connection = new SqlConnection(ConnectionString);
        return (await connection.QueryFirstAsync<TEntity>(command));
    }

    public async Task<int> ExecuteAsync(string command)
    {
        await using var connection = new SqlConnection(ConnectionString);
        return (await connection.ExecuteAsync(command));
    }

}