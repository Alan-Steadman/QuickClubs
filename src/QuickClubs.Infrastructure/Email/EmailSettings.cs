namespace QuickClubs.Infrastructure.Email;

public class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public bool EmailEnabled { get; set; }
    /// <summary>
    /// If set, sends all email to this address.  Leave as empty string in production.
    /// </summary>
    public string DebugEmailTo { get; set; } = null!;
    public string DefaultFromAddress { get; set; } = null!;
    public string DefaultFromDisplayName { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
