using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using QuickClubs.Application.Abstractions.Authentication;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Authentication.Common;
using QuickClubs.Application.Authentication.Register;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Users;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.UnitTests.Authentication;

public class RegisterCommandHandlerTests
{
    private readonly IUserRepository _userRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IPasswordHasher _passwordHasherMock;
    private readonly IJwtTokenGenerator _jwtTokenGeneratorMock;
    private readonly IDateTimeProvider _dateTimeProviderMock;

    private static readonly string FirstName = "alan";
    private static readonly string LastName = "test";
    private static readonly string Email = "alan.test@email.com";
    private static readonly string Password = "Password123";

    public RegisterCommandHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _passwordHasherMock = Substitute.For<IPasswordHasher>();
        _jwtTokenGeneratorMock = Substitute.For<IJwtTokenGenerator>();
        _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
    }

    [Fact]
    public async Task Handle_Should_ReturnAuthenticationResult_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .ReturnsNull();

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().BeOfType<AuthenticationResult>();
        result.Value.FirstName.Should().Be(FirstName);
        result.Value.LastName.Should().Be(LastName);
        result.Value.Email.Should().Be(Email);
        result.Value.Token.Should().BeOfType<string>();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .Returns(User.Create(
                new FirstName(""),
                new LastName(""),
                new UserEmail(""),
                new PasswordHash("")));

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.DuplicateEmail);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .ReturnsNull();

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        _userRepositoryMock.Received(1)
            .Add(Arg.Is<User>(u => u.Email.Value == Email));
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .ReturnsNull();

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        await _unitOfWorkMock.Received(1)
            .SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_ShouldNot_CallUnitOfWork_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .Returns(User.Create(
                new FirstName(""),
                new LastName(""),
                new UserEmail(""),
                new PasswordHash("")));

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        await _unitOfWorkMock.Received(0)
            .SaveChangesAsync(default);
    }

    [Fact]
    public async Task Handle_Should_CallPasswordHasher_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .ReturnsNull();

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        _passwordHasherMock.Received(1)
            .Hash(Arg.Is<string>(x => x == Password));
    }

    [Fact]
    public async Task Handle_Should_CallJwtTokenGenerator_WhenEmailIsUnique()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, Password);

        _userRepositoryMock.GetByEmailAsync(Email, default)
            .ReturnsNull();

        var handler = new RegisterCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock,
            _passwordHasherMock,
            _jwtTokenGeneratorMock,
            _dateTimeProviderMock);

        // Act
        Result<AuthenticationResult> result = await handler.Handle(command, default);

        // Assert
        _jwtTokenGeneratorMock.Received(1)
            .GenerateToken(Arg.Is<User>(u => u.Email.Value == Email));
    }

}
