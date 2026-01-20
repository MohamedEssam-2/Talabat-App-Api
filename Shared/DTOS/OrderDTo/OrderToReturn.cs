using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.IdentityDto;

namespace Shared.DTOS.OrderDTo
{
    public class OrderToReturn
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethod { get; set; } = null!;
        public string Status { get; set; } = null!;
        public AddressDto ShipToAddress { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
