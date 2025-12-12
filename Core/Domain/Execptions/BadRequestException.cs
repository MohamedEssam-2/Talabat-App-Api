using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Execptions
{
    public sealed class BadRequestException(List<string>errors) :Exception("Validation Failed")
    {
        public List<string> Errors { get; } = errors;  // store the errors in a property

        //   public BadRequestException(List<string> errors)
        //: base("Validation Failed")
        //   {
        //       Errors = errors;
        //   }
    }
}
