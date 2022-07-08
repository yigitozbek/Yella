using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yella.Framework.Utilities.Results;
using Yella.Order.Data.Orders.Dtos;

namespace Yella.Order.Data.Orders.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDTO?> GetByIdAsync(Guid id);
        Task<IDataResult<OrderItemDTO>> AddAsync(OrderItemDTO input);
        Task<IDataResult<OrderItemDTO>> UpdateAsync(OrderItemDTO input);
        Task<IResult> DeleteAsync(Guid id);

    }
}
