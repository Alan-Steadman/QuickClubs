using FluentValidation;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Application.Memberships.ApproveMembership;

public sealed class ApproveMembershipCommandValidator : AbstractValidator<ApproveMembershipCommand>
{
    public ApproveMembershipCommandValidator()
    {
        RuleFor(x => x.Reason).MaximumLength(Approval.ReasonMaxLength);
    }
}
