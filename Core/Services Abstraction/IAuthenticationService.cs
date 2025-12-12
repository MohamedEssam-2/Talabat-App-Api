using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOS.IdentityDto;

namespace Services_Abstraction
{
    public interface IAuthenticationService
    {
        //Login
        Task<UserDto> LoginAsync(LoginDto loginDto);
        //Register
        Task<UserDto> RegisterAsync(RegisterDto registerDto);

        //CheckEmail
        Task<bool> CheckEmailAsync(string email);

        //Get Current User
        Task<UserDto> GetCurrentUserAsync(string email);

        //Get Current USer Address
        Task<AddressDto> GetCurrentUserAddress(string email );


        // Create or Update User Address
        Task<AddressDto> UpdateUserAddress(string email,AddressDto addressDto);

    }
}
