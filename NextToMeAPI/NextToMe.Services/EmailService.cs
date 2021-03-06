﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NextToMe.Services
{
    public class EmailService : IEmailService
    {
        private const string _emailConfirmTemplateFile = "../emailTemplates/invitation.html";
        private const string _emailResetPasswordTemplateFile = "../emailTemplates/reseting.html";
        private const string _emailLinkToReplace = "-link-";
        private const string _senderEmail = "nexttome.noreply@gmail.com";
        private const string _senderName = "NextToMe NoReply";
        private const string _senderPassword = "Abc1234!";
        private const string _mailSmtpServer = "smtp.gmail.com";

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public EmailService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IHttpContextAccessor contextAccessor,
            UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public string GetInvitationMessage(string redirectUrl)
        {
            var pathToTemplate = Path.Combine(System.Reflection.Assembly.GetCallingAssembly().Location, _emailConfirmTemplateFile);
            var htmlText = File.ReadAllText(pathToTemplate);
            htmlText = htmlText.Replace(_emailLinkToReplace, redirectUrl);
            return htmlText;
        }

        public string GetResetMessage(string redirectUrl)
        {
            var pathToTemplate = Path.Combine(System.Reflection.Assembly.GetCallingAssembly().Location, _emailResetPasswordTemplateFile);
            var htmlText = File.ReadAllText(pathToTemplate);
            htmlText = htmlText.Replace(_emailLinkToReplace, redirectUrl);
            return htmlText;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var fromAddress = new MailAddress(_senderEmail, _senderName);
            var toAddress = new MailAddress(to);
            const string fromPassword = _senderPassword;

            var smtp = new SmtpClient
            {
                Host = _mailSmtpServer,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            await smtp.SendMailAsync(message);
        }
    }
}
