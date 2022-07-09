using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Domain.Dto;
using Yella.Utilities.Results;

namespace Yella.Dapper;

public interface IDapperRepository
{
    static string? ConnectionString;
    IResult Connection(string connectionString);

    Task<List<TEntity>> GetListAsync<TEntity>(string command) where TEntity : class;
    Task<TEntity> GetAsync<TEntity>(string command) where TEntity : class;
    Task<int> ExecuteAsync(string command);
}