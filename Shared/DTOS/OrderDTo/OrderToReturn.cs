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
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethod { get; set; } = null!;
        public AddressDto address { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
    }
}
