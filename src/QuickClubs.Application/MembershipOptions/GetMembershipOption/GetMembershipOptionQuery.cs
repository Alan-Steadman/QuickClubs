using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.MembershipOptions.Common;

namespace QuickClubs.Application.MembershipOptions.GetMembershipOption;

public sealed record GetMembershipOptionQuery(Guid Id) : IQuery<MembershipOptionResult>;