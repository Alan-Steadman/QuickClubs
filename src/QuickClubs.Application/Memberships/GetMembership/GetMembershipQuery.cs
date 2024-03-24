using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Memberships.Common;

namespace QuickClubs.Application.Memberships.GetMembership;

public record GetMembershipQuery(Guid Id) :IQuery<MembershipResult> ;