using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sportshall.Core.DTO;
using Sportshall.Core.Entites;
using Sportshall.Core.interfaces;
using Sportshall.Core.Services;
using Sportshall.Core.Sharing;
using Sportshall.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries
{
    public class AuthRepositry : IAuth
    {

      
        private readonly UserManager<AppUser> userManager;
        private readonly IEmailService emailService;
        private readonly SignInManager<AppUser> signInManager;

        private readonly IGenerateToken token;
        public AuthRepositry(UserManager<AppUser> userManager, IEmailService _emailService, SignInManager<AppUser> _signInManager, IGenerateToken token)
        {
            this.userManager = userManager;
            emailService = _emailService;
            signInManager = _signInManager;
            this.token = token;
        }

        public async Task<string> LoginUser(LoginDTO loginDTO)
        {

            if (loginDTO == null)
            {
                return null;
            }

           

            var finduser = await userManager.FindByEmailAsync(loginDTO.Email);

            //if (!finduser.EmailConfirmed)
            //{

            //    string token = await userManager.GenerateEmailConfirmationTokenAsync(finduser);
            //    await SendEmail(finduser.Email, token, "active", "active email", "please active your email");
            //    return "please active your email first";

            //}

            var result = await signInManager.CheckPasswordSignInAsync(finduser, loginDTO.Password, true);

            if (result.Succeeded )
            {
                return token.GetAndCreateToken(finduser);
            }


            //generate token 




            return "email or password is not correct";
        }

        public async Task<string> RegisterUser(RegisterDTO registerDTO)
        {

            if (registerDTO == null)
            {
                return null;
            }

            if (await userManager.FindByNameAsync(registerDTO.UserName) is not null)
            {
                return "this user name is already exist";
            }

            if (await userManager.FindByEmailAsync(registerDTO.Email) is not null)
            {
                return "this email is already exist";
            }

            AppUser appUser = new AppUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
            };

            var result = await userManager.CreateAsync(appUser, registerDTO.Password);

            if (result.Succeeded is not true )
            {
                return result.Errors.ToList()[0].Description;
            }

            // send active emai
            // l

            //string token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);

            //await SendEmail(appUser.Email, code, "active", "active email", "please active your email");

            return "done";
            
        }

        public async  Task SendEmail(string email,string code, string component,string subject,string message)
        {

            var  result = new EmailDTO(email,"radouanebareche6@gmail.com",subject
                ,EmailStringBody.Send(email,code,component,message)
                
                );

            await emailService.SendEmail(result);

        }

        public async Task<bool> SendEmailForForgetPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            string token = await userManager.GeneratePasswordResetTokenAsync(user);

            await SendEmail(user.Email, token, "Reset-password", "Reset password", "please reset your password");

            return true;
        }


        public async Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
            {
                return null;
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.Password);

            if (result.Succeeded)
            {
                return "password change successfully";
            }

            return result.Errors.ToList()[0].Description ;
        }


        public async Task<bool> ConfirmEmail(ActiveAccountDTO activeAccountDTO)
        {
            var user = await userManager.FindByEmailAsync(activeAccountDTO.Email);
            if (user == null)
            {
                return false;
            }

            var result = await userManager.ConfirmEmailAsync(user, activeAccountDTO.Token);

            if (!result.Succeeded)
            {
               var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await SendEmail(user.Email, token, "active", "active email", "please active your email again");
                return false;
            }



            return result.Succeeded;
        }







    }
}
