using Application.Abstractions.Messaging;

namespace Application.Auth.ResendOTP;
public sealed record ResendOtpCommand(string email) : ICommand<Guid>;
