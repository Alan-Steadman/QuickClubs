namespace QuickClubs.Application.Abstractions.Email;

public class EmailMessage : IEmailMessage
{
    public List<string> ToEmail { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }

    public EmailMessage(List<string> toEmail, string subject, string body)
    {
        ToEmail = toEmail;
        Subject = subject;
        Body = body;
    }
}