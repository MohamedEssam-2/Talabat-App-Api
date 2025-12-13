using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
        protected string GetEmailFromToken()
        {
            return User.Claims
                .FirstOrDefault(c =>
                    c.Type == ClaimTypes.Email ||
                    c.Type == "email" ||
                    c.Type == "Email")?.Value
                ?? throw new UnauthorizedAccessException("User is not authenticated");
        }



    }
}
