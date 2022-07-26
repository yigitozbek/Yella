using Yella.AutoMapper.Extensions;
using Yella.EntityFrameworkCore;
using Yella.EntityFrameworkCore.Models;
using Yella.Order.Data.Orders.Dtos;
using Yella.Order.Data.Orders.Interfaces;
using Yella.Order.Domain.Orders;
using Yella.Utilities.Results;

namespace Yella.Order.Application.Modules.Orders.Services
{
    public class OrderItemApplicationService : IOrderItemService
    {
        private readonly IRepository<OrderItem, Guid> _orderItemRepository;
        private readonly IRepository<Demand, Guid> _demandRepository;
        public OrderItemApplicationService(IRepository<OrderItem, Guid> orderItemRepository, IRepository<Demand, Guid> demandRepository)
        {
            _orderItemRepository = orderItemRepository;
            _demandRepository = demandRepository;
        }

        public async Task<List<OrderItemDTO>> GetListAsync(PaginationFilter filter)
        {
            var test = await _demandRepository.GetListAsync(filter);

            var query = await _orderItemRepository.GetListAsync(filter);
            return query.ToMapper<List<OrderItemDTO>>();
        }

        public async Task<IDataResult<OrderItemDTO>> AddAsync(OrderItemDTO input)
        {
            var result = await _orderItemRepository.AddAsync(input.ToMapper<OrderItem>());

            if (!result.Success)
            {
                return new ErrorDataResult<OrderItemDTO>(result.Message);
            }

            return new SuccessDataResult<OrderItemDTO>(result.Message);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var result = await _orderItemRepository.DeleteAsync(id);

            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            return new SuccessResult(result.Message);

        }

        public async Task<OrderItemDTO?> GetByIdAsync(Guid id)
        {
            var query = await _orderItemRepository.GetAsync(id);
            return query.ToMapper<OrderItemDTO>();
        }

        public async Task<IDataResult<OrderItemDTO>> UpdateAsync(OrderItemDTO input)
        {
            var result = await _orderItemRepository.UpdateAsync(input.ToMapper<OrderItem>());

            if (!result.Success)
            {
                return new ErrorDataResult<OrderItemDTO>(result.Message);
            }

            return new SuccessDataResult<OrderItemDTO>(result.Message);
        }
    }
}
