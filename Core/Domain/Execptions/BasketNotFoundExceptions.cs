using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public class BasketNotFoundExceptions (string key): NotFoundException($"Basket With This Key {key} is Not Found")
    {
        //public BasketNotFoundExceptions(string message) : base($"Basket With This Key {key} is Not Found")
        //{
        //}
    }
}
