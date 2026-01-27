using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys;

namespace DomainLayer.Models.OrderModels
{
    public class Order:BaseEntity<Guid> //Globally Unique Identifier , used for unique identification across different systems((a3f2d7c4-9b1e-45b3-bf55-08d123abc9e7)), do not need auto-increment in database

    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, int deliveryMethodId, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethodId = deliveryMethodId;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderAddress Address { get; set; } = null!; //Owned Entity 
        public int DeliveryMethodId { get; set; } //Foreign Key
        public ICollection<OrderItem> Items { get; set; } = [];//Navigation Property 1 order :M OrderItems
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } //  Pending= 0,   PaymentReceived = 1, PaymentFailed = 2,
        public DeliveryMethod DeliveryMethod { get; set; } = null!;//Navigation Property 1 order :M DeliveryMethod

        [NotMapped]
        public decimal Total { get => SubTotal + DeliveryMethod.Price; } //NotMapped Attribute to exclude this property from being mapped to a database column as it is a computed property.

        public string PaymentIntentId { get; set; }
    }
}
