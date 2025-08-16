using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProject
{
    internal class EmailManagement
    {
        public static void sendEmail(string receipientEmail, string attachmentFilePath)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("asaphdelcoro98@gmail.com"));
            email.To.Add(MailboxAddress.Parse(receipientEmail));
            email.Subject = "Test Email";

            var multipart = new Multipart("mixed");

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(File.OpenRead(attachmentFilePath)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(attachmentFilePath)
            };

            multipart.Add(attachment);
            email.Body = multipart;

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("asaphdelcoro987@gmail.com", "xgyx bsuk lnfr htks");
            Debug.WriteLine(smtp.Send(email));
            Debug.WriteLine(receipientEmail);
            smtp.Disconnect(true);
        }
    }
}
