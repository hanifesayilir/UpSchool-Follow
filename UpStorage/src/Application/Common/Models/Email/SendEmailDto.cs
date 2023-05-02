using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models.Email
{
    public class SendEmailDto
    {

        public List<string> EmailAddresses { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public SendEmailDto(List<string> emailAddress, string content, string subject)
        {
            EmailAddresses= emailAddress;

            Content = content;

            Subject = subject;
        }

        public SendEmailDto(string emailAddress, string content, string subject)
        {
            EmailAddresses = new List<string> { emailAddress};

            Content = content;

            Subject = subject;
        }


    }
}
