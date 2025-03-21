using Application.Abstractions.Messaging;
using Domain.DTO.Auth;

namespace Application.Auth.VerifyUser;
public sealed record VerifyUserCommand(string email, string otp) : ICommand<CreatedUserDto>;
