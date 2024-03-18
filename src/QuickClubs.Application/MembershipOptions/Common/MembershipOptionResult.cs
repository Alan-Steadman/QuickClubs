namespace QuickClubs.Application.MembershipOptions.Common;
public sealed class MembershipOptionResult
{
    // Technical Debt: remove this class and use the record instead
    // The reason this is a class not a record is that using Dapper
    // I need to add the list of membership levels to the membership
    // option in a separate operation after the init.

    public Guid Id { get; set; }
    public Guid ClubId { get; set; }
    public string Name { get; set; }
    public string Period { get; set; }
    public bool HasCutoff { get; set; }
    public int? CutoffMonth { get; set; }
    public int? CutoffDay { get; set; }
    public List<MembershipLevelResult> Levels { get; set; }

    public MembershipOptionResult(
        Guid id,
        Guid clubId,
        string name,
        string period,
        bool hasCutoff,
        int? cutoffMonth,
        int? cutoffDay,
        List<MembershipLevelResult> levels)
    {
        Id = id;
        ClubId = clubId;
        Name = name;
        Period = period;
        HasCutoff = hasCutoff;
        CutoffMonth = cutoffMonth;
        CutoffDay = cutoffDay;
        Levels = levels;
    }

    public MembershipOptionResult(
        Guid id,
        Guid clubId,
        string name,
        string period,
        bool hasCutoff,
        int? cutoffMonth,
        int? cutoffDay)
    {
        Id = id;
        ClubId = clubId;
        Name = name;
        Period = period;
        HasCutoff = hasCutoff;
        CutoffMonth = cutoffMonth;
        CutoffDay = cutoffDay;
        Levels = new List<MembershipLevelResult>();
    }
}


public sealed record MembershipLevelResult(
    Guid MembershipLevelId,
    Guid MembershipOptionId,
    string Name,
    string Description,
    int MaxMembers,
    int? MinAge,
    int? MaxAge,
    decimal PriceAmount);
