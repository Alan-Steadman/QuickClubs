namespace QuickClubs.ArchitectureTests.Presentation;

public class PresentationDependencyTests : BaseTest
{
    [Fact]
    public void Controllers_Should_Have_DependencyOnMediatR()
    {
        // Arrange
        var assembly = PresentationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
