using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Core.Domain.Dto;

namespace Yella.Core.Dapper;

public interface IDapperRepository
{
    Task<List<TEntity>> GetListAsync<TEntity>(string command)
        where TEntity : EntityDto;
    Task<TEntity> GetAsync<TEntity>(string command)
        where TEntity : EntityDto;
    Task<int> ExecuteAsync(string command);
}