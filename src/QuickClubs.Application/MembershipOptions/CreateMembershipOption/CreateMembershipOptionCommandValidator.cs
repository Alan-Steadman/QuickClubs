using FluentValidation;
using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Application.MembershipOptions.CreateMembershipOption;

internal sealed class CreateMembershipOptionCommandValidator : AbstractValidator<CreateMembershipOptionCommand>
{
    public CreateMembershipOptionCommandValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty()
            .MaximumLength(MembershipOptionName.MaxLength);

        RuleFor(o => o.Period)
            .NotEmpty()
            .Must(p => MembershipPeriod.All.Any(all => all.ToString() == p))
                .WithMessage("The membership period is not recognised");

        RuleFor(o => o.CutoffMonth)
            .InclusiveBetween(1, 12).When(o => o.HasCutoff);

        RuleFor(o => o.CutoffDay)
            .InclusiveBetween(1, 31).When(o => o.HasCutoff); // TODO: check max day according to which month it is

        RuleFor(o => o.Levels)
            .Must(o => o.Count > 0)
                .WithMessage("There must be at least one membership level");

        RuleForEach(x => x.Levels).ChildRules(level =>
        {
            level.RuleFor(x => x.Name)
                .MaximumLength(MembershipLevelName.MaxLength);

            level.RuleFor(x => x.Description)
                .MaximumLength(MembershipLevelDescription.MaxLength);

            level.RuleFor(x => x.MaxMembers)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(10);

            level.RuleFor(x => x.MinAge)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100);

            level.RuleFor(x => x.MaxAge)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(100);

            level.RuleFor(x => x.MaxAge)
                .GreaterThanOrEqualTo(x => x.MinAge).When(x => x.MinAge != null && x.MaxAge != null)
                .WithMessage("Max Age must be greater than Min Age");

            level.RuleFor(x => x.PriceAmount)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10000);
        });
    }
}
