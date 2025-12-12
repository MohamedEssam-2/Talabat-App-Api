using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public sealed class UserNotfoundException(string email) : NotFoundException($"User with this Email : {email} is Not Found ")
    {

    }
}
