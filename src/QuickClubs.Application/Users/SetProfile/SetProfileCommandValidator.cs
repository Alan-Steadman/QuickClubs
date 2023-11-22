using FluentValidation;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Users.Entities;

namespace QuickClubs.Application.Users.SetProfile;

internal sealed class SetProfileCommandValidator : AbstractValidator<SetProfileCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    public SetProfileCommandValidator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.AddYears(UserProfile.MinYearsOld * -1)))
            .GreaterThan(DateOnly.FromDateTime(_dateTimeProvider.UtcNow.AddYears(UserProfile.MaxYearsOld * -1)));

        RuleFor(x => x.Building)
            .NotEmpty()
            .MaximumLength(Address.BuildingMaxLength);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(Address.StreetMaxLength);

        RuleFor(x => x.Locality)
            .MaximumLength(Address.LocalityMaxLength);

        RuleFor(x => x.Town)
            .NotEmpty()
            .MaximumLength(Address.TownMaxLength);

        RuleFor(x => x.County)
            .MaximumLength(Address.CountyMaxLength);

        RuleFor(x => x.Postcode)
            .NotEmpty()
            .MaximumLength(Address.PostcodeMaxLength);
    }
}
