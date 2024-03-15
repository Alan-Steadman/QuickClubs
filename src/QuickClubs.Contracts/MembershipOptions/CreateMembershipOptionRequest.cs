namespace QuickClubs.Contracts.MembershipOptions;

public sealed record CreateMembershipOptionRequest(
    string Name,
    string Period, // daily/weekly/monthly/yearly
    bool HasCutoff,
    byte CutoffMonth,
    byte CutoffDay,
    List<CreateMembershipLevel> Levels);

public sealed record CreateMembershipLevel(
    string Name,
    string Description,
    int MaxMembers,
    int? MinAge,
    int? MaxAge,
    decimal PriceAmount);