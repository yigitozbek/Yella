using Yella.Domain.Dto;

namespace Yella.Order.Data.Orders.Dtos;

public class OrderItemDTO : FullAuditedEntityDto<Guid>
{
    public OrderItemDTO()
    {
        IsActive = true;
    }

    public decimal Amount { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public short CurrencyId { get; set; }

    public bool IsActive { get; set; }
}
