using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.Sharing
{
    public class EmailStringBody
    {

        public static string Send(string email,string token,string component,string message)
        {
            string encodeToken = Uri.EscapeDataString(token);

            return $@"<html>
                 

                     <head>
                        <meta charset='utf-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1'>
                        <title>Sportshall</title>
                    </head>
<body>

<footer>
<p>Dear {email},</p>
<p>{message}</p>
<p>To {component}, please click the link below:</p>
<p><a href=""https://localhost:4200/Acount/{component}?email={email}&code={encodeToken}"">Click here to {component}</a></p>
<p>If you did not request this action, please ignore this email.</p>
<p>Thank you for using our service!</p>
<p>Best regards,</p>
<p>Sportshall Team</p>
</footer>
</body>
</html>


          ";



        }



    }
}
