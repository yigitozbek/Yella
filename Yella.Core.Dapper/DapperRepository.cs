using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Dapper;

public class DapperRepository : IDapperRepository
{
    public readonly string ConnectionString;

    public DapperRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public async Task<List<TEntity>> GetListAsync<TEntity>(string command) 
        where TEntity : EntityDto
    {
        await using var connection = new SqlConnection(ConnectionString);
        return (await connection.QueryAsync<TEntity>(command)).AsList();
    }

    public async Task<TEntity> GetAsync<TEntity>(string command)
        where TEntity : EntityDto
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