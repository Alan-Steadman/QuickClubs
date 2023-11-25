using MediatR;
using QuickClubs.Application.Abstractions.Email;
using QuickClubs.Domain.Users.Events;
using QuickClubs.Domain.Users.Repository;
using System.Net.Mail;

namespace QuickClubs.Application.Authentication.Register;

internal sealed class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UserCreatedDomainEventHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId);
        if (user is null)
            return;

        var message = new EmailMessage(
            [user.Email.Value],
            "Verification",
            "Please verify your email address using code 123");

        await _emailService.SendEmailAsync(message);

    }
}
