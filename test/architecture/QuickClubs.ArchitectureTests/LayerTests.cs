using FluentAssertions;
using NetArchTest.Rules;

namespace QuickClubs.ArchitectureTests;

public class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOn_OtherLayers()
    {
        // Arrange
        var assembly = DomainAssembly;

        var otherLayers = new[]
        {
            ApplicationAssembly.GetName().Name,
            InfrastructureAssembly.GetName().Name,
            PresentationAssembly.GetName().Name,
            ContractsAssembly.GetName().Name,
            WebAssembly.GetName().Name
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAll(otherLayers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_NotHaveDependencyOn_OtherLayers()
    {
        // Arrange
        var assembly = ApplicationAssembly;

        var otherLayers = new[]
        {
            InfrastructureAssembly.GetName().Name,
            PresentationAssembly.GetName().Name,
            ContractsAssembly.GetName().Name,
            WebAssembly.GetName().Name
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAll(otherLayers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_NotHaveDependencyOn_OtherLayers()
    {
        // Arrange
        var assembly = InfrastructureAssembly;

        var otherLayers = new[]
        {
            PresentationAssembly.GetName().Name,
            ContractsAssembly.GetName().Name,
            WebAssembly.GetName().Name
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAll(otherLayers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Presentation_Should_NotHaveDependencyOn_OtherLayers()
    {
        // Arrange
        var assembly = PresentationAssembly;

        var otherLayers = new[]
        {
            DomainAssembly.GetName().Name,
            ApplicationAssembly.GetName().Name,
            InfrastructureAssembly.GetName().Name,
            ContractsAssembly.GetName().Name,
            WebAssembly.GetName().Name
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAll(otherLayers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Contracts_Should_NotHaveDependencyOn_OtherLayers()
    {
        // Arrange
        var assembly = ContractsAssembly;

        var otherLayers = new[]
        {
            DomainAssembly.GetName().Name,
            ApplicationAssembly.GetName().Name,
            InfrastructureAssembly.GetName().Name,
            PresentationAssembly.GetName().Name,
            WebAssembly.GetName().Name
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAll(otherLayers)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
