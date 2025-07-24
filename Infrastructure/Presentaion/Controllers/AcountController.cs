using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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

        [Authorize]
        [HttpPost("logout")]
        public ActionResult Logout()
        {
            return Ok(new { message = "Logged out successfully. Please delete the token on client side." });
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

        [HttpGet("login-google")]
        public ActionResult GoogleLogin()
        {
            var Prop = new AuthenticationProperties
            {
                RedirectUri = "/api/auth/google-response"
            };
            return Challenge(Prop, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<ActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return Unauthorized();

            var claims = result.Principal.Claims;

            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            
            var user = await _serviceManager.AuthenticationServices.GetCreanteUserAsync(email);

          
            if (user == null)
            {
                var newUser = new RegisterDto
                {
                    DisplayName = name,
                    Email = email,
                    PhoneNumber = "", 
                    UserName = email.Split('@')[0],
                    Password = Guid.NewGuid().ToString() 
                };
                user = await _serviceManager.AuthenticationServices.RegisterAsync(newUser);
            }

            return Ok(user);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            try
            {
                await _serviceManager.AuthenticationServices.ForgotPasswordAsync(model.Email, model.ClientAppUrl);
                return Ok("Reset password link has been sent to your email.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            try
            {
                await _serviceManager.AuthenticationServices.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
                return Ok("Password has been reset successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
