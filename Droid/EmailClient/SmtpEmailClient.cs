using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Dustbuster.Droid.EmailClient;
using Dustbuster.Interfaces;

[assembly: Dependency(typeof(SmtpEmailClient))]
namespace Dustbuster.Droid.EmailClient
{
    public class SmtpEmailClient : IEmailClientConnect
    {
        public Task<Boolean> SendSMTPMail(String SubjectValue, String EmailValue)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.Subject = SubjectValue;

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }


            return Task.FromResult(true);
        }

    }
        
}