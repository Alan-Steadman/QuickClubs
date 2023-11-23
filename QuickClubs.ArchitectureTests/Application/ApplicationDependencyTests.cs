namespace QuickClubs.ArchitectureTests.Application;

public class ApplicationDependencyTests : BaseTest
{
    [Fact]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        // Act
        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainAssembly.GetName().Name)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
