using Application.Abstractions.Messaging;
using Domain.DTO.Auth;

namespace Application.Auth.Login;
public sealed record LoginUserCommand(string Email, string Password) : ICommand<LoginSuccessDto>;
