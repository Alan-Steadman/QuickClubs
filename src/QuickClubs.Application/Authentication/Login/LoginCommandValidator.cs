using FluentValidation;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Authentication.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
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
