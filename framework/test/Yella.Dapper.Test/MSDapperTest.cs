using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Dapper.Test.Entities;

namespace Yella.Dapper.Test
{
    [TestClass]
    public class MSDapperTest
    {
        private readonly IDapperRepository _dapperRepository;
        public MSDapperTest(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public async Task<List<Country>> GetPerson()
        {
            var query = await _dapperRepository.GetListAsync<Country>("SELECT TOP (1000) [Id], [Name], [Code] FROM [dbo].[Countries]");
            return query;
        }


    }
}
