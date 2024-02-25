using QuickClubs.Domain.Clubs.Events;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.ClubTypes;

namespace QuickClubs.Domain.UnitTests.Clubs;

public class ClubTests : BaseTest
{
    [Fact]
    public void CreateClub_Should_Raise_ClubCreatedDomainEvent()
    {
        // Arrange
        var clubType = ClubType.FromCode("TMC");
        var name = new ClubName("Trumpton Motor Club", "TMC");
        var website = new ClubWebsite("https://www.tmc.org.uk/");

        // Act
        var club = Club.Create(
            clubType!,
            name,
            website);

        // Assert
        var clubCreatedDomainEvent = AssertDomainEventWasPublished<ClubCreatedDomainEvent>(club);
    }
}
