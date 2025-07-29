using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.interfaces;
using System.Linq;
using System.Security.Claims;

namespace Sportshall.Api.Controllers
{

    public class AccountController : BaseController
    {
        public AccountController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var result = await work.Auth.RegisterUser(registerDto);
            if (result != "done")
            {
                return BadRequest(new ResponseApi(400,result));
             
            }

            return Ok(new ResponseApi(200, result));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var result = await work.Auth.LoginUser(loginDto);


            

            //if (result.StartsWith("please"))
            //{
            //    return BadRequest(new ResponseApi(400, result));

            //}


           
            Response.Cookies.Append("token", result.token, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTimeOffset.UtcNow.AddDays(1),

                IsEssential = true, // Make the cookie essential for the application to function
                // Change this to your domain
                SameSite = SameSiteMode.Strict,
                
            });

            return Ok( result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Sign out from ASP.NET Identity
            //await signInManager.SignOutAsync();

            // Remove the JWT cookie
            Response.Cookies.Delete("token", new CookieOptions
            {
                Domain = "localhost", // Make sure this matches the domain you set in login
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new ResponseApi(200, "Logged out successfully"));
        }






        [HttpPost("reset-password")]


        public async Task<IActionResult> ResetPassword([FromBody] string email)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var result = await work.Auth.SendEmailForForgetPassword(email);

            
            return result ? Ok(new ResponseApi(200, "Please check your email to reset your password."))
                : BadRequest(new ResponseApi(400, "Email not found or already reset."));
        }

        [HttpGet("get-all-users")]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await work.Auth.GetallUsers();
            if (users == null)
            {
                return NotFound(new ResponseApi(404, "No users found."));
            }

            return Ok( users);
        }

        [HttpGet("get-user-by-id/{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await work.Auth.GetUserById(id);
            if (user == null)
            {
                return NotFound(new ResponseApi(404, "User not found."));
            }

            return Ok(user);
        }
        [HttpDelete("delete-user/{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await work.Auth.DeleteUser(id);
            if (user == null)
            {
                return NotFound(new ResponseApi(404, "User not found."));
            }

            return Ok(new ResponseApi(200, "User deleted successfully."));
        }






        [Authorize]
        [HttpGet("myinfo")]
        public IActionResult GetMe()
        {
            // Access user claims from JWT token
            var username = User.Identity?.Name; // usually maps to ClaimTypes.Name
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                userId,
                username,
                email,
                role
            });
        }

        //[HttpGet("me")]
        //public async Task<IActionResult> GetMe()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    if (user == null)
        //        return Unauthorized();

        //    var roles = await _userManager.GetRolesAsync(user);

        //    return Ok(new
        //    {
        //        name = user.UserName, // or user.FullName
        //        email = user.Email,
        //        role = roles.FirstOrDefault()
        //    });
        //}






    }
}
