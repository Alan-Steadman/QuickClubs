using QuickClubs.Application.Abstractions.Clock;

namespace QuickClubs.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
