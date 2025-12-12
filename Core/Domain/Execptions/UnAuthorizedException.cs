using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public sealed class UnAuthorizedException(string msg="Invalid Email Or password") :Exception(msg)
    {
    }
}
