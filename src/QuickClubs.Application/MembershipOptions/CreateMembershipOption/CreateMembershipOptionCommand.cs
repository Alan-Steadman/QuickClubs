using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.MembershipOptions.CreateMembershipOption;

public sealed record CreateMembershipOptionCommand(
    Guid ClubId,
    string Name,
    string Period, // daily/weekly/monthly/yearly
    bool HasCutoff,
    int CutoffMonth,
    int CutoffDay,
    List<CreateMembershipLevel> Levels) : ICommand<Guid>;

public sealed record CreateMembershipLevel(
    string Name,
    string Description,
    int MaxMembers,
    int? MinAge,
    int? MaxAge,
    decimal PriceAmount);