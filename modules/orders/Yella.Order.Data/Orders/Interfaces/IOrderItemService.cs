using Yella.Order.Data.Orders.Dtos;
using Yella.Utilities.Results;

namespace Yella.Order.Data.Orders.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemDTO?> GetByIdAsync(Guid id);
        Task<List<OrderItemDTO>> GetListAsync();
        Task<IDataResult<OrderItemDTO>> AddAsync(OrderItemDTO input);
        Task<IDataResult<OrderItemDTO>> UpdateAsync(OrderItemDTO input);
        Task<IResult> DeleteAsync(Guid id);

    }
}
