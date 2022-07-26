using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
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

    public class Demand : FullAuditedEntity<Guid>, ICompanyBase
    {
        public Demand()
        {
            IsActive = true;
            Date = DateTime.Now;
        }



        [MaxLength(50)]
        public string Number { get; set; }


        public Guid CompanyId { get; set; }



        public Guid CustomerId { get; set; }



        public DateTime Date { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }


        public bool IsActive { get; set; }
    }



}
