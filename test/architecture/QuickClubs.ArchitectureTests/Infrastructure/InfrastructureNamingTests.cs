using Microsoft.EntityFrameworkCore;
using QuickClubs.Infrastructure.Persistence.Repositories;

namespace QuickClubs.ArchitectureTests.Infrastructure;

public class InfrastructureNamingTests : BaseTest
{
    [Fact]
    public void Repositories_Should_Have_RepositorySuffix()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .Inherit(typeof(Repository<,>))
            .Should().HaveNameEndingWith("Repository")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void EfConfigurations_Should_Have_ConfigurationSuffix()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .That()
            .ImplementInterface(typeof(IEntityTypeConfiguration<>))
            .Should().HaveNameEndingWith("Configuration")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
