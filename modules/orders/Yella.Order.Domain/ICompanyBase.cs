using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yella.Order.Domain
{
    public interface ICompanyBase
    {
        public Guid CompanyId { get; set; }

    }

    public class CompanyBase : ICompanyBase
    {
        public Guid CompanyId { get; set; }
    }

}
