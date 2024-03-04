using FluentValidation;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Application.Locations.CreateLocation;

public sealed class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(LocationName.MaxLength);

        // TODO: Validate Address fields

        RuleFor(x => x.OsGridRef)
            .MaximumLength(OsGridRef.MaxLength);

        RuleFor(x => x.WhatThreeWords)
            .MaximumLength(WhatThreeWords.MaxLength);

        RuleFor(x => x.Directions)
            .MaximumLength(Directions.MaxLength);
    }
}
