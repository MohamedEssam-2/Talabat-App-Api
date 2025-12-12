using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public sealed class DeliveryMethodNotFoundException(int id ) :NotFoundException($"Delivery Method with Id {id} was not found.")
    {
    }
}
