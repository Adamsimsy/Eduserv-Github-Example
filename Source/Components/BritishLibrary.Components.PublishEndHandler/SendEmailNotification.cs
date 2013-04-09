using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Items;

namespace BritishLibrary.Components.PublishEndHandler
{
    public class SendEmailNotification
    {
        public SendEmailNotification()
        {

        }

        public void Send(object sender, EventArgs args)
        {           
            try
            {
                var publishArgs = args as Sitecore.Events.SitecoreEventArgs;

                var publisher = publishArgs.Parameters[0] as Sitecore.Publishing.Publisher;

                var publisherOptions = publisher.Options;

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("localhost");

                mail.From = new MailAddress("noreply@example.com");
                mail.To.Add("johnsmith@example.com");
                mail.Subject = "Someone published the item " + publisherOptions.RootItem.Paths.FullPath;
                mail.Body = publisherOptions.RootItem.Fields["body"].Value;
                SmtpServer.Send(mail);

                string lines = publisherOptions.RootItem.Fields["body"].Value;

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\Git\\Eduserv-Github-Example\\Source\\Website\\BritishLibrary.Website\\test.xml");
                file.WriteLine(lines);

                file.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Seems some problem!");
            }
        }
    }
}
