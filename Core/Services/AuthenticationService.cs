using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitys.IdentityModels;
using Domain.Execptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services_Abstraction;
using Shared.Authenticaion;
using Shared.DTOS.IdentityDto;
using Shared.Authenticaion;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Diagnostics.Eventing.Reader;

namespace Services_Layer
{
    internal class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IOptions<JwtOptions> _jwtOptions , IMapper _mapper) : IAuthenticationService
    {
     

        public async Task<UserDto> LoginAsync(LoginDto loginDto) //login dto contains real Email and Password from Controllers
        {
            //Check if Email/Password exists
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) { throw new UserNotfoundException(loginDto.Email); }

            //Check if Password is correct
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password); //compare hashed password
            if (!isPasswordValid) { throw new UnAuthorizedException(); }
            else
            {
                //password is Valid 
                return new UserDto
                {
                    Email = user.Email!,
                    DispalyName = user.DisplayName,
                    Token = await CreateTokenAsync(user),
                };
            }

        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto) //register dto contains real Email , Password , DisplayName from Controllers
        {
            //passing Data of the new User(Register dto) in ApplicationUser Entity
            var user = new ApplicationUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Email = user.Email!,
                    DispalyName = user.DisplayName,
                    Token = await CreateTokenAsync(user),
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
                #region Returned Error
                //{
                //  "message": "Validation Failed",
                //  "errors": [
                //    "Username 'test@example.com' is already taken.",
                //    "Password must be at least 6 characters."
                //  ]
                //} 
                #endregion
            }
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var JwtOptions = _jwtOptions.Value;
            //claims =>information about the user used in the token 
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!),
            };
            var roles =await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //var Secretkey = _configuration["JwtOptions:SecretKey"]; //used in decoding the token
            var Secretkey = JwtOptions.SecretKey;      //used in decoding the token


            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secretkey));

            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

            #region Old Way 
                   //issuer: "My Issuer Back End",
                   //issuer: _configuration["JwtOptions:Issuer"],
                   //audience: _configuration["JwtOptions:Audience"],
                   //claims: claims,
                   //expires: DateTime.Now.AddHours(1),
                   //signingCredentials: credentials
            #endregion
            #region New way using JwtOptions Class
                   //issuer: JwtOptions.Issuer,
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(JwtOptions.DurationInDays),
                signingCredentials: credentials
            #endregion

                );
           return  new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) 
                ?? throw new UserNotfoundException(email);
            return new UserDto()
            {
                Email = user.Email!,
                DispalyName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u=>u.Email==email) 
            ?? throw new UserNotfoundException(email);
            if (user.Address is not null)
            {
                return _mapper.Map<AddressDto>(user.Address);


            }
            else
            {
                throw new AddressNotFound(user.UserName);
            }
        }

        public async Task<AddressDto> UpdateUserAddress(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email)
             ?? throw new UserNotfoundException(email);
            if (user.Address is not null)//Update in Address (in database IQueryable)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Street = addressDto.Street;
                user.Address.Country = addressDto.Country;

            }
            else//Create
            {
                user.Address = _mapper.Map<Address>(addressDto); //Create addresss in db and the data comes from address dto , reverse mapping
            }
           await _userManager.UpdateAsync(user); //add or update address in db
            return _mapper.Map<AddressDto>(user.Address);
        }
    }
}
