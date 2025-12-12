using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.OrderModels
{
    public enum OrderStatus //Payment Status
    {
        Pending= 0,
        PaymentReceived = 1,
        PaymentFailed = 2,
    }
}
