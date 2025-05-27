using Sportshall.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.interfaces
{
    public interface IAuth
    {


        public Task<string> RegisterUser(RegisterDTO registerDTO);

        public Task<string> LoginUser(LoginDTO loginDTO);

        public Task SendEmail(string email, string code, string component, string subject, string message);

        public Task<bool> SendEmailForForgetPassword(string email);

        public  Task<string> ResetPassword(ResetPasswordDTO resetPasswordDTO);


        public Task<bool> ConfirmEmail(ActiveAccountDTO activeAccountDTO);



    }
}
