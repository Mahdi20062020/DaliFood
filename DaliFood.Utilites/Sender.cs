using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DaliFood.Utilites
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new();
            SmtpClient SmtpServer = new("smtp.gmail.com");
            mail.From = new MailAddress("configureemailspad@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("configureemailspad@gmail.com", "@Mahdi2006");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return Task.CompletedTask;
        }
       
    }
    //public class SmsService
    //{
    //    public Task SendAsync(string token,string message,string phonenumber)
    //    {   
           
    //    }
    //}
}
