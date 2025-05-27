using Sportshall.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailDTO emailDTO);
    }
}
