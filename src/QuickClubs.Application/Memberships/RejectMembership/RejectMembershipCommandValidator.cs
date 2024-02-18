using FluentValidation;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Application.Memberships.RejectMembership;

public sealed class RejectMembershipCommandValidator : AbstractValidator<RejectMembershipCommand>
{
    public RejectMembershipCommandValidator()
    {
        RuleFor(x => x.Reason).NotEmpty();
        RuleFor(x => x.Reason).MaximumLength(Approval.ReasonMaxLength);
    }
}
