using Yella.Domain.Entities;

namespace Yella.Order.Domain.Orders
{
    public class OrderItem : FullAuditedEntity<Guid>, ICompanyBase
    {
        public OrderItem()
        {
            IsActive = true;
        }

        public decimal Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public short CurrencyId { get; set; }

        public bool IsActive { get; set; }

        public Guid CompanyId { get; set; }
    }
}
