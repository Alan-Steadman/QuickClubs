namespace QuickClubs.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(IEmailMessage message);
}
