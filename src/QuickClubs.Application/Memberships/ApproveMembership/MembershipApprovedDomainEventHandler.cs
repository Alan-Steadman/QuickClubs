using MediatR;
using QuickClubs.Application.Abstractions.Email;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Memberships.Events;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Users;
using QuickClubs.Domain.Users.Repository;
using System.Text;

namespace QuickClubs.Application.Memberships.ApproveMembership;

internal sealed class MembershipApprovedDomainEventHandler : INotificationHandler<MembershipApprovedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IClubRepository _clubRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;

    public MembershipApprovedDomainEventHandler(
        IEmailService emailService,
        IClubRepository clubRepository,
        IMembershipRepository membershipRepository,
        IUserRepository userRepository)
    {
        _emailService = emailService;
        _clubRepository = clubRepository;
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(MembershipApprovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var membership = await _membershipRepository.GetByIdAsync(notification.membershipId, cancellationToken);

        if (membership is null)
            return;

        var club = await _clubRepository.GetByIdAsync(membership.ClubId, cancellationToken);
        if (club is null)
            return;

        foreach (var userId in membership.Members)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is not null)
            {
                string subject = $"{club.Name.Acronym} Membership Approved";
                StringBuilder message = new StringBuilder();

                message.Append($"<p>Congratulations, your membership application to {club.Name.FullName} has been approved.</p>");
                message.Append($"<p>Your membership number is {membership.MembershipNumber}.</p>");
                message.Append($"<p>You can view your memberships online at ...</p>"); // TODO: include link to clubengine or club website

                var emailMessage = new EmailMessage(
                    [user.Email.Value],
                    subject,
                    message.ToString());

                await _emailService.SendEmailAsync(emailMessage);
            }
        }
    }
}
