using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SportAdvisorAPI.Helpers
{
    public class SendMail
    {
        public async Task SendEmailAsync(string name, string email, string emailToken, string verifyToken)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sports Advisor", "info@sports-advisor.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = "Sending with Twilio SendGrid is Fun";
            var link = "http://localhost:5054/api/Account/verify?token=" + verifyToken;

            string htmlTemplate = @$"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Confirm Your Email</title>
            </head>
            <body style=""font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 20px; text-align: center;"">
                <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);"">
                    <h1 style=""color: #333;"">Welcome to Sports Advisor!</h1>
                    <p style=""font-size: 16px; color: #555;"">Hello {name},</p>
                    <p style=""font-size: 16px; color: #555;"">Thank you for registering with Sports Advisor. Please click the button below to confirm your email:</p>
                    <a href=""{link}"" style=""display: inline-block; padding: 10px 20px; background-color: #007bff; color: #ffffff; text-decoration: none; border-radius: 5px; margin-top: 20px;"">Confirm Email</a>
                    <p style=""font-size: 14px; color: #777; margin-top: 20px;"">If you didn't register with Sports Advisor, you can safely ignore this email.</p>
                </div>
            </body>
            </html>";


            var bodyBuilder = new BodyBuilder
            {
                // TextBody = "and easy to do anywhere, especially with C#",
                HtmlBody = htmlTemplate
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            // SecureSocketOptions.StartTls forces a secure connection over TLS[^2^][2]
            await client.ConnectAsync("smtp.sendgrid.net", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                userName: "apikey", // The userName is the exact string "apikey" and not the API key itself.
                password: emailToken
            );

            // Send the email
            await client.SendAsync(message);

            // Disconnect from the server
            await client.DisconnectAsync(true);
        }
    }
}
