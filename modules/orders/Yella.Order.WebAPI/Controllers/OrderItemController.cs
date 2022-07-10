using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yella.Order.Data.Orders.Dtos;
using Yella.Order.Data.Orders.Interfaces;

namespace Yella.Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet(nameof(GetList))]
        public async Task<List<OrderItemDTO>> GetList()
        {
            var result = await _orderItemService.GetListAsync();
            return result;
        }
    }
}
