using FluentValidation.TestHelper;
using QuickClubs.Application.Authentication.Register;

namespace QuickClubs.Application.UnitTests.Authentication;
public class RegisterCommandValidatorTests
{
    private static readonly string FirstName = "alan";
    private static readonly string LastName = "test";
    private static readonly string Email = "alan.test@email.com";
    private static readonly string Password = "Password123";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenFirstNameIsBlank(string firstName)
    {
        // Arrange
        var command = new RegisterCommand(firstName, LastName, Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenFirstNameIsTooLong()
    {
        // Arrange
        var command = new RegisterCommand("1234567890123456789012345678901", LastName, Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenFirstNameIsTooShort()
    {
        // Arrange
        var command = new RegisterCommand("a", LastName, Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenLastNameIsBlank(string lastName)
    {
        // Arrange
        var command = new RegisterCommand(FirstName, lastName, Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenLastNameIsTooLong()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, "1234567890123456789012345678901", Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenLastNameIsTooShort()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, "a", Email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenEmailIsBlank(string email)
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, email, Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenEmailIsInvalidFormat()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, "invalid.email", Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenEmailIsTooLong()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890@email.com", Password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenPasswordIsBlank(string password)
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, password);
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenPasswordIsTooShort()
    {
        // Arrange
        var command = new RegisterCommand(FirstName, LastName, Email, "1234567");
        var validator = new RegisterCommandValidator();

        // Act
        TestValidationResult<RegisterCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    // TODO: Add test for password regex, once one is implemented in QuickClubs.Domain.Users.ValueObjects
}
