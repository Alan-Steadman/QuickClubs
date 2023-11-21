using FluentValidation;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Authentication.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .Length(FirstName.MinLength, FirstName.MaxLength);

        RuleFor(x => x.LastName)
            .Length(LastName.MinLength, LastName.MaxLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(UserEmail.MaxLength);

        RuleFor(x => x.Password)
            .MinimumLength(PasswordHash.PasswordMinLength);
        RuleFor(x => x.Password)
            .Matches(PasswordHash.PasswordRegex)
            .WithMessage(PasswordHash.PasswordRegexMessage);
    }
}
