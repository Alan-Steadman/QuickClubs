namespace QuickClubs.Contracts.MembershipOptions;
public sealed record MembershipOptionResponse(
    Guid Id,
    Guid ClubId,
    string Name,
    string Period, // daily/weekly/monthly/yearly
    bool HasCutoff,
    int CutoffMonth,
    int CutoffDay,
    List<MembershipLevelResponse> Levels);

public sealed record MembershipLevelResponse(
    Guid MembershipLevelId,
    string Name,
    string Description,
    int MaxMembers,
    int? MinAge,
    int? MaxAge,
    decimal PriceAmount);