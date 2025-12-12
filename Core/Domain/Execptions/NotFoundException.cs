using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public abstract class NotFoundException(string message):Exception(message)
    {
        //public NotFoundException(string message) : base(message)
        //{
        //}
    }
}
