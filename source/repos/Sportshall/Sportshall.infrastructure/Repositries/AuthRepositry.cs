using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IGenerateToken token;

        private readonly IMapper _mapper;
        public AuthRepositry(IMapper _mapper, UserManager<AppUser> userManager, IEmailService _emailService, SignInManager<AppUser> _signInManager, IGenerateToken token, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            emailService = _emailService;
            signInManager = _signInManager;
            this.token = token;
            this._mapper = _mapper;
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager), "RoleManager is null in AuthRepositry constructor.");
        }

        //public async Task<string> LoginUser(LoginDTO loginDTO)
        //{
        //    try
        //    {
        //        if (loginDTO == null)
        //        {
        //            return null;
        //        }

        //        var finduser = await userManager.FindByEmailAsync(loginDTO.Email);


        //        if (finduser == null)
        //        {
        //            return "Email or password is not correct";
        //        }

        //        // Uncomment if you want to enforce email confirmation
        //        /*
        //        if (!finduser.EmailConfirmed)
        //        {
        //            string token = await userManager.GenerateEmailConfirmationTokenAsync(finduser);
        //            await SendEmail(finduser.Email, token, "active", "active email", "please activate your email");
        //            return "Please activate your email first";
        //        }
        //        */

        //        var result = await signInManager.CheckPasswordSignInAsync(finduser, loginDTO.Password, true);

        //        if (result.Succeeded)
        //        {
        //            return await token.GetAndCreateToken(finduser);
        //        }

        //        return "Email or password is not correct";

        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Exception: {ex.Message} | {ex.StackTrace}";
        //    }

        //}

        public async Task<UserResponse> LoginUser(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                    return null;

                var finduser = await userManager.FindByEmailAsync(loginDTO.Email);
                if (finduser == null)
                    return null;

                var userdto= _mapper.Map<UserDTO>(finduser);

                var result = await signInManager.CheckPasswordSignInAsync(finduser, loginDTO.Password, true);
                if (result.Succeeded)
                {
                    
                    await signInManager.SignInAsync(finduser, isPersistent: true);

                   string thetoken= await token.GetAndCreateToken(finduser);


                    return new UserResponse(userdto, thetoken);
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle properly in production
                throw new Exception("Login failed: " + ex.Message);
            }
        }



        public async Task<string> RegisterUser(RegisterDTO registerDTO)
        {
            try
            {
                if (registerDTO == null)
                    return "Invalid input.";

                if (string.IsNullOrWhiteSpace(registerDTO.UserName) ||
                    string.IsNullOrWhiteSpace(registerDTO.Email) ||
                    string.IsNullOrWhiteSpace(registerDTO.Password))
                    return "All fields are required.";

                if (await userManager.FindByNameAsync(registerDTO.UserName) != null)
                    return "This username is already in use.";

                if (await userManager.FindByEmailAsync(registerDTO.Email) != null)
                    return "This email is already in use.";

                var appUser = new AppUser
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                    DisplayName = registerDTO.DisplayName
                };

                var result = await userManager.CreateAsync(appUser, registerDTO.Password);

                if (!result.Succeeded)
                    return result.Errors.FirstOrDefault()?.Description ?? "User creation failed";

                var role = string.IsNullOrWhiteSpace(registerDTO.Role) ? "User" : registerDTO.Role;

                if (roleManager == null)
                    return "Role manager is not initialized.";

                if (!await roleManager.RoleExistsAsync(role))
                    return $"Role '{role}' does not exist.";

                await userManager.AddToRoleAsync(appUser, role);

                return "done";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message} | {ex.StackTrace}";
            }
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

      

        public async Task<IEnumerable<UserDTODetails>> GetallUsers()
        {
            var users = await userManager.Users.ToArrayAsync();    // needs Microsoft.EntityFrameworkCore
            return _mapper.Map<List<UserDTODetails>>(users);


        }

        public async Task<UserDTODetails> GetUserById(Guid id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return null;                       // Framework will wrap this in a Task

            return _mapper.Map<UserDTODetails>(user);


        }

        public async Task<UserDTODetails> DeleteUser(Guid id)
        {

            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return null;

            // Framework will wrap this in a Task

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return null;

            return _mapper.Map<UserDTODetails>(user);





        }
    }
}
