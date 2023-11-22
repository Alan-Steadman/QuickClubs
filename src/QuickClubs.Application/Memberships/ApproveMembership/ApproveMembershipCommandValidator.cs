using FluentValidation;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Application.Memberships.ApproveMembership;

internal sealed class ApproveMembershipCommandValidator : AbstractValidator<ApproveMembershipCommand>
{
    public ApproveMembershipCommandValidator()
    {
        RuleFor(x => x.Reason).MaximumLength(Approval.ReasonMaxLength);
    }
}
