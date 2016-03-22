using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using BookStore.Domain.Abstract;
using BookStore.Domain.Entities;

namespace BookStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "paul.pernici@amtrustgroup.com";
        public string MailFromAddress = "bookstore@example.com";
        public bool UseSSL = true;
        public string Username = "username";
        public string Password = "password";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\22267\Desktop";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {

                /// Do email setup.
                smtpClient.EnableSsl = emailSettings.UseSSL;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }


                // Begin email body.
                StringBuilder body = new StringBuilder();

                body.AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");


                // List all items with prices.
                foreach (CartLine line in cart.Lines)
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity, line.Product.Name, line.Product.Price * line.Quantity);


                // List shipping address.
                body.AppendFormat("Total:\t{0}", cart.ComputeTotal());
                body.AppendLine("Ship to:");
                body.AppendLine(shippingDetails.Name);
                body.AppendLine(shippingDetails.AddressLine1);
                body.AppendLine(shippingDetails.AddressLine2 ?? "");
                body.AppendLine(shippingDetails.AddressLine3 ?? "");
                body.AppendFormat("{0}, {1} {2}\n", shippingDetails.City, shippingDetails.State, shippingDetails.Zip);
                body.AppendLine(shippingDetails.Country);


                // Create and send message.
                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, emailSettings.MailToAddress, "New Order", body.ToString());

                if (emailSettings.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.ASCII;

                smtpClient.Send(mailMessage);
            }
        }
    }
}
