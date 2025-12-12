using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public class AddressNotFound(string username):NotFoundException($"User: {username} Has No Address")
    {
    }
}
