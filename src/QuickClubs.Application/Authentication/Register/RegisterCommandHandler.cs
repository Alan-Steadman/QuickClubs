using QuickClubs.Application.Abstractions.Authentication;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Authentication.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Users;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Authentication.Register;
internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterCommandHandler(
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

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check user doesn't already exist
        if (await _userRepository.GetByEmailAsync(request.Email) is not null)
        {
            return Result.Failure<AuthenticationResult>(UserErrors.DuplicateEmail);
        }

        // Create Password Hash
        var passwordHash = _passwordHasher.Hash(request.Password);

        // Create User
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new UserEmail(request.Email),
            new PasswordHash(passwordHash)
        );
        user.SetLastLoginNow(_dateTimeProvider.UtcNow);
        user.SetRegisteredNow(_dateTimeProvider.UtcNow);

        // Generate Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // Persist user
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();

        // Return AuthenticationResult
        return new AuthenticationResult(
            user.Id.ToString(),
            user.FirstName.Value,
            user.LastName.Value,
            user.Email.Value,
            token);
    }
}
