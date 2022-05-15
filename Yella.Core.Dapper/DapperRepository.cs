using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archseptia.Core.Domain.Dto;
using Dapper;
using Microsoft.Extensions.Configuration;
using Yella.Core.Dapper;

namespace Archseptia.Core.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        private readonly string _connectionString;

        public DapperRepository(IConfiguration configuration) => _connectionString = configuration["ConnectionStrings:SqlServer"];

        public async Task<List<TEntity>> GetListAsync<TEntity>(string command) 
            where TEntity : EntityDto
        {
            await using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryAsync<TEntity>(command)).AsList();
        }

        public async Task<TEntity> GetAsync<TEntity>(string command)
            where TEntity : EntityDto
        {
            await using var connection = new SqlConnection(_connectionString);
            return (await connection.QueryFirstAsync<TEntity>(command));
        }

        public async Task<int> ExecuteAsync(string command)
        {
            await using var connection = new SqlConnection(_connectionString);
            return (await connection.ExecuteAsync(command));
        }

    }
}
