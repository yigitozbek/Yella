using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yella.EntityFrameworkCore.Models;
using Yella.Order.Data.Orders.Dtos;
using Yella.Order.Data.Orders.Interfaces;

namespace Yella.Order.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<OrderItemDTO>> GetList(PaginationFilter filter)
        {
            var result = await _orderItemService.GetListAsync(filter);
            return result;
        }
    }
}
