using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sportshall.Api.Helper;
using Sportshall.Core.DTO;
using Sportshall.Core.interfaces;

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

            if (result.StartsWith("please"))
            {
                return BadRequest(new ResponseApi(400, result));

            }


           
            Response.Cookies.Append("token", result, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Domain = "localhost",
                Expires = DateTimeOffset.UtcNow.AddDays(1),

                IsEssential = true, // Make the cookie essential for the application to function
                // Change this to your domain
                SameSite = SameSiteMode.Strict,
                
            });

            return Ok(new ResponseApi(200, result));
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






    }
}
