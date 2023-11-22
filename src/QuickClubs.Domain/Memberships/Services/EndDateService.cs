using QuickClubs.Domain.MembershipOptions;

namespace QuickClubs.Domain.Memberships.Services;

public sealed class EndDateService
{
    public DateTime CalculateEndDate(MembershipOption membershipOption, DateTime utcNow)
    {
        DateTime endDate;

        switch (membershipOption.Period.ToString())
        {
            case "Daily":
                endDate = utcNow.AddDays(1); break;
            case "Weekly":
                endDate = utcNow.AddDays(7); break;
            case "Monthly":
                endDate = utcNow.AddMonths(1); break;
            case "Yearly":
                endDate = utcNow.AddYears(1); break;
            default: throw new ApplicationException("Unrecognised membership period");
        }

        if (membershipOption.Cutoff is not null)
        {
            var cutoffDate = new DateTime(utcNow.Year, membershipOption.Cutoff.Month, membershipOption.Cutoff.Day);
            if (cutoffDate > endDate)
            {
                cutoffDate.AddYears(-1);
            }
            endDate = cutoffDate;
        }

        return endDate;
    }
}
