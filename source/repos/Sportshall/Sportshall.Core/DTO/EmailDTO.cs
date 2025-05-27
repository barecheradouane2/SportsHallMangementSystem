using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.Core.DTO
{
    public class EmailDTO
    {
        public EmailDTO(string to, string from, string subject, string content)
        {
            To = to;
            From = from;
            Subject = subject;
            this.content = content;
        }

        public string To { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string content { get; set; }
    }
}
