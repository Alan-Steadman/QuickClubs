namespace QuickClubs.Infrastructure.Email;

public class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public string DefaultFromAddress { get; set; } = null!;
    public string DefaultFromDisplayName { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
