using QuickClubs.Domain.Abstractions;

namespace QuickClubs.ArchitectureTests.Domain;

public class DomainNamingTests : BaseTest
{
    [Fact]
    public void DomainEvent_Should_Have_DomainEventSuffix()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should().HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
