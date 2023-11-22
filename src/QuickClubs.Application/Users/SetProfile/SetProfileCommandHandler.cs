using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Users.SetProfile;

internal sealed class SetProfileCommandHandler : ICommandHandler<SetProfileCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetProfileCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SetProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound);
        }

        var address = new Address(
            request.Building,
            request.Street,
            request.Locality,
            request.Town,
            request.County,
            request.Postcode);

        var result = user.SetProfile(
            request.DateOfBirth,
            address);

        if (result.IsFailure)
        {
            return result;
        }

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
