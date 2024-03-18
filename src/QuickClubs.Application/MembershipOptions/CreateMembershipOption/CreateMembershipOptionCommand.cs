using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.MembershipOptions.Common;

namespace QuickClubs.Application.MembershipOptions.CreateMembershipOption;

public sealed record CreateMembershipOptionCommand(
    Guid ClubId,
    string Name,
    string Period, // daily/weekly/monthly/yearly
    bool HasCutoff,
    int CutoffMonth,
    int CutoffDay,
    List<CreateMembershipLevel> Levels) : ICommand<MembershipOptionResult>;

public sealed record CreateMembershipLevel(
    string Name,
    string Description,
    int MaxMembers,
    int? MinAge,
    int? MaxAge,
    decimal PriceAmount);