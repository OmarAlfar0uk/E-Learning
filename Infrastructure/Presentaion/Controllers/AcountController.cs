using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Share.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class AcountController(IServiceManager _serviceManager) : BaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User =await _serviceManager.AuthenticationServices.LoginAsync(loginDto);
            return Ok(User);
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user =await _serviceManager.AuthenticationServices.RegisterAsync(registerDto);
            return Ok(user);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheakEmailAsync(string Email)
        {
            var Result = await _serviceManager.AuthenticationServices.CheckEmailAsync(Email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationServices.GetCreanteUserAsync(email);
            return Ok(AppUser);
        }

    }
}
