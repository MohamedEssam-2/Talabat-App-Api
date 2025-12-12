using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.DTOS.IdentityDto;

namespace Presentation_Layer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] //BaseURL/api
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        //Login URL => BaseUrl/api/AuthenticationController/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =await serviceManager.authenticationService.LoginAsync(loginDto);
            return Ok(user);
        }
        //Register URL => BaseUrl/api/AuthenticationController/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await serviceManager.authenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var res = await serviceManager.authenticationService.CheckEmailAsync(email);
            return Ok(res);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email=User.FindFirst(ClaimTypes.Email)?.Value;
            var AppUser = await serviceManager.authenticationService.GetCurrentUserAddress(email!);
            return Ok(AppUser);
        }
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var AppUser = await serviceManager.authenticationService.GetCurrentUserAddress(email!);
            return Ok(AppUser);
        }
        [HttpPut("Address")]
        public async Task<ActionResult<UserDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var UpdatedAddress = await serviceManager.authenticationService.UpdateUserAddress(email!, addressDto);
            return Ok(UpdatedAddress);
        }

    }
}
