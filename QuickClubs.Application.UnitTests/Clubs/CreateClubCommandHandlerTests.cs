using FluentAssertions;
using NSubstitute;
using QuickClubs.Application.Clubs.CreateClub;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;

namespace QuickClubs.Application.UnitTests.Clubs;
public class CreateClubCommandHandlerTests
{
    private readonly IClubRepository _clubRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    private static readonly string FullName = "Trumpton Motor Club";
    private static readonly string Acronym = "TMC";
    private static readonly string Website = "https://www.tmc.org.uk/";

    public CreateClubCommandHandlerTests()
    {
        _clubRepositoryMock = Substitute.For<IClubRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenFullNameIsNotUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(false);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(true);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(true);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClubErrors.DuplicateFullName);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenAcronymIsNotUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(true);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(false);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(true);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClubErrors.DuplicateAcronym);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenWebsiteIsNotUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(true);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(true);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(false);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(ClubErrors.DuplicateWebsite);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenEverythingIsUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(true);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(true);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(true);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenEverythingIsUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(true);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(true);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(true);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        _clubRepositoryMock.Received(1)
            .Add(Arg.Is<Club>(c => c.Name.FullName == FullName)); // (assert that teh club repository mock recieved 1 call to the Add method with an argument of a club with the same full name as the full name of the club we created)
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWorkSaveChanges_WhenFullNameIsNotUnique()
    {
        // Arrange
        var command = new CreateClubCommand(FullName, Acronym, Website);

        _clubRepositoryMock.IsFullNameUniqueAsync(null, FullName, default)
            .Returns(false);
        _clubRepositoryMock.IsAcronymUniqueAsync(null, Acronym, default)
            .Returns(true);
        _clubRepositoryMock.IsWebsiteUniqueAsync(null, Website, default)
            .Returns(true);

        var handler = new CreateClubCommandHandler(_clubRepositoryMock, _unitOfWorkMock);

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        await _unitOfWorkMock.Received(0)
            .SaveChangesAsync(default); // (assert that Save Changes Async was not called (was called zero times))
    }
}
