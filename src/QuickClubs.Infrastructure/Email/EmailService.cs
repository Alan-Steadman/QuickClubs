using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using QuickClubs.Application.Abstractions.Email;

namespace QuickClubs.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly EmailSettings _emailSettings;
    public EmailService(
        ILogger<EmailService> logger,
        IOptions<EmailSettings> emailOptions)
    {
        _logger = logger;
        _emailSettings = emailOptions.Value;
    }

    public async Task SendEmailAsync(IEmailMessage message)
    {
        var emailMessage = CreateEmailMessage(message);
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(IEmailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailSettings.DefaultFromDisplayName, _emailSettings.DefaultFromAddress));
        if (_emailSettings.DebugEmailTo == "")
            emailMessage.To.AddRange(message.ToEmail.ConvertAll(email => new MailboxAddress(email, email)));
        else
            emailMessage.To.Add(new MailboxAddress("DebugEmailTo", _emailSettings.DebugEmailTo));

        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Body };
        
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        if (!_emailSettings.EmailEnabled)
        {
            _logger.LogInformation($"Email not sent to {mailMessage.To.ToString()} because EnableEmail is set to false in email settings");
            return;
        }

        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(mailMessage);
                // TODO: Structured logging!
                _logger.LogInformation($"Email sent to: {mailMessage.To.ToString()}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendEmail failed to sent to: {mailMessage.To.ToString()} with exception: {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
