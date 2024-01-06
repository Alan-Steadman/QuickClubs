using FluentValidation.TestHelper;
using QuickClubs.Application.Clubs.CreateClub;

namespace QuickClubs.Application.UnitTests.Clubs;

public class CreateClubCommandValidatorTests
{
    string FullName = "Trumpton Motor Club";
    string Acronym = "TMC";
    string Website = "https://www.tmc.org.uk/";

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenFullNameIsBlank(string fullName)
    {
        // Arrange
        var command = new CreateClubCommand(fullName, Acronym, Website);
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenFullNameIsTooLong()
    {
        // Arrange
        var command = new CreateClubCommand("123456789012345678901234567890123456789012345678901", Acronym, Website);
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FullName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenAcronymIsBlank(string acronym)
    {
        // Arrange
        var command = new CreateClubCommand(FullName, acronym, Website);
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Acronym);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenAcronymIsTooLong()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, "123456789", Website);
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Acronym);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validator_ShouldReturnFalse_WhenWebsiteIsBlank(string website)
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, website);
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Website);
    }

    [Fact]
    public void Validator_ShouldReturnFalse_WhenWebsiteIsTooLong()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, "123456789012345678901234567890123456789012345678901234567890123456789012345678901");
        var validator = new CreateClubCommandValidator();

        // Act
        TestValidationResult<CreateClubCommand> result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Website);
    }
}
