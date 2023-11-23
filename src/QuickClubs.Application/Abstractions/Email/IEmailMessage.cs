namespace QuickClubs.Application.Abstractions.Email;
public interface IEmailMessage
{
    public List<string> ToEmail { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }
}
