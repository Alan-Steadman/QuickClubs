using FluentValidation;
using QuickClubs.Domain.Common;

namespace QuickClubs.Application.Clubs.SetAffiliated;

public sealed class SetAffiliatedCommandValidator : AbstractValidator<SetAffiliatedCommand>
{
    public SetAffiliatedCommandValidator()
    {
        RuleFor(x => x.CurrencyCode)
            .NotEmpty()
            .MaximumLength(Currency.CodeMaxLength)
            .Must(c => Currency.All.Any(all => all.Code == c)).WithMessage("The currency is not supported");
    }
}
