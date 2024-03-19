using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.MembershipOptions.Common;

namespace QuickClubs.Application.MembershipOptions.GetAllMembershipOptions;

public sealed record GetAllMembershipOptionsQuery(Guid ClubId) : IQuery<IEnumerable<MembershipOptionResult>>;
