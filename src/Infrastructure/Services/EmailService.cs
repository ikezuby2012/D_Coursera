using System.Globalization;
using Application.Abstractions.Interface;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private string? GetSmtpPort()
    {
        return _configuration["Email:SmtpPort"];
    }

    #region EMAIL BODY GENERATOR
    private static string PopulateOTPHtml(string name, string otp)
    {
        string baseDirectory = Directory.GetCurrentDirectory();
        string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);

        string filePath = Path.Combine(baseDirectory, "HTMLTemplates", "SendOTPEmail.html");
        string body = string.Empty;

        using var reader = new StreamReader(filePath);
        body = reader.ReadToEnd();

        body = body.Replace("[OTP]", otp);
        body = body.Replace("[NAME]", name);
        body = body.Replace("[YEAR]", year);

        return body;

    }
    #endregion

    private async Task SendMailAsync(string email, string name, string subject, string body, CancellationToken cancellationToken = default)
    {
        string? smtpPortConfig = GetSmtpPort();
        int smtpPort = int.Parse(smtpPortConfig!, CultureInfo.InvariantCulture);

        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration["Email:FromName"], _configuration["Email:From"]));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        client.CheckCertificateRevocation = false;

        await client.ConnectAsync(_configuration["Email:SmtpHost"], smtpPort, SecureSocketOptions.Auto, cancellationToken);

        await client.AuthenticateAsync(_configuration["Email:Username"], _configuration["Email:Password"], cancellationToken);

        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }

    public async Task SendWelcomeMail(string to, string subject, string name, string otp, CancellationToken cancellationToken = default)
    {
        string body = PopulateOTPHtml(name, otp);
        await SendMailAsync(to, name, subject, body, cancellationToken);
    }
}
