using FluentValidation;
using QuickClubs.Domain.Clubs.ValueObjects;

namespace QuickClubs.Application.Clubs.CreateClub;

internal sealed class CreateClubCommandValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubCommandValidator()
    {
        RuleFor(cmd => cmd.FullName)
            .NotEmpty()
            .MaximumLength(ClubName.FullNameMaxLength);

        RuleFor(cmd => cmd.Acronym)
            .NotEmpty()
            .MaximumLength(ClubName.AcronymMaxLength);

        RuleFor(cmd => cmd.Website)
            .NotEmpty()
            .MaximumLength(ClubWebsite.MaxLength);
    }
}
