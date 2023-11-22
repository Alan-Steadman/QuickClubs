using QuickClubs.Application.Abstractions.Authentication;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Authentication.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;

namespace QuickClubs.Application.Authentication.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<AuthenticationResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Check if user exists
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            return Result.Failure<AuthenticationResult>(UserErrors.InvalidCredentials);
        }

        // Check password
        if (!_passwordHasher.Verify(user.PasswordHash.Value, request.Password))
        {
            return Result.Failure<AuthenticationResult>(UserErrors.InvalidCredentials);
        }

        // Generate Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Set Last Login
        user.SetLastLoginNow(_dateTimeProvider.UtcNow);

        // Persist user
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        // return AuthenticationResult
        return new AuthenticationResult(
            user.Id.ToString(),
            user.FirstName.Value,
            user.LastName.Value,
            user.Email.Value,
            token);

    }
}
