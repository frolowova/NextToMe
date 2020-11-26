using NextToMe.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IEmailService
    {
        public string GetInvitationMessage(string redirectUrl);
        public string GetResetMessage(string redirectUrl);
        public Task SendEmailAsync(string to, string subject, string body);
    }
}
