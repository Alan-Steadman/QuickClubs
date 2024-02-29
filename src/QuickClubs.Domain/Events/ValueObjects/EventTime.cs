namespace QuickClubs.Domain.Events.ValueObjects;
public record EventTime(DateTime Time, string Name, string? Description)
{
    public const int NameMaxLength = 30;
    public const int DescriptionMaxLength = 300;
}
