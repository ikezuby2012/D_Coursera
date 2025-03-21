using Application.Abstractions.Interface;
using Domain.Users;
using MediatR;

namespace Application.Auth.Register;

internal sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IEmailService _emailService;
    public UserRegisteredDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send an email verification OTP.
        await _emailService.SendWelcomeMail(to: notification.user.Email, subject: "OTP verification code", name: notification.user.FirstName, otp: notification.user.OTP, cancellationToken);
    }
}
