namespace QuickClubs.Domain.MembershipOptions.ValueObjects;

public sealed record MembershipPeriod()
{
    public static readonly MembershipPeriod Daily = new MembershipPeriod("Daily", "Day", false);
    public static readonly MembershipPeriod Weekly = new MembershipPeriod("Weekly", "Week", false);
    public static readonly MembershipPeriod Monthly = new MembershipPeriod("Monthly", "Month", false);
    public static readonly MembershipPeriod Yearly = new MembershipPeriod("Yearly", "Year", true);

    private MembershipPeriod(
        string durationName,
        string periodName,
        bool cutoffApplicable) : this()
    {
        DurationName = durationName;
        PeriodName = periodName;
        CutoffApplicable = cutoffApplicable;
    }

    public string DurationName { get; init; } = null!;
    public string PeriodName { get; init; } = null!;
    public DateTimeOffset Duration { get; init; }
    public bool CutoffApplicable { get; init; }

    public static MembershipPeriod FromString(string durationName)
    {
        return All.FirstOrDefault(p => p.DurationName == durationName) ??
            throw new ApplicationException("The membership period is invalid");
    }

    public override string ToString()
    {
        return DurationName;
    }

    public const int MaxLength = 8;

    public static readonly IReadOnlyCollection<MembershipPeriod> All = new[]
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    };
}
