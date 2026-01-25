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
        //Use Primary Constructor rather than this constructor

        //The message passed to the exception is stored in the base Exception class
        //and later consumed by global exception handling middleware to generate an appropriate HTTP response(Json Response of the Msg and Status code)
    }
}
