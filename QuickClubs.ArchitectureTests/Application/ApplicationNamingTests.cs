using FluentValidation;
using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.ArchitectureTests.Application;

public class ApplicationNamingTests : BaseTest
{
    [Fact]
    public void CommandHandler_Should_Have_CommandHandlerSuffix()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Exception_Should_Have_ExceptionSuffix()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandler_Should_Have_QueryHandlerSuffix()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Validator_Should_Have_ValidatorSuffix()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
