﻿using Domain.Excptions;
using Domain.Models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Share.DataTransferObject.IdentityDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthenticationServices(UserManager<AppUsers> _userManager, IConfiguration _configuration) : IAuthenticationServices
    {

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user =await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
            var IsPasowrdValid =await _userManager.CheckPasswordAsync(user , loginDto.Password);
            if (IsPasowrdValid)
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token =await CreateTokenAsync(user)
                };
            else
                throw new UnAuthorizedException();

            
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new AppUsers()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };

            var Result =await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
                return new UserDto
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequsestException(Errors);
            }
        }
        private async Task<string> CreateTokenAsync(AppUsers user)
        {
            var Claims = new List<Claim>()
            {   
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.NameIdentifier , user.Id!),

            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Creds
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User is not null;
        }

        public async Task<UserDto> GetCreanteUserAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);
            return new UserDto() { DisplayName = User.DisplayName, Email = User.Email, Token = await CreateTokenAsync(User) };

        }
    }
}
