using Application.Abstractions.Messaging;
using Domain.DTO.Auth;

namespace Application.Auth.Register;
public sealed record RegisterUserCommand(string Email, string FirstName, string LastName, string Password) : ICommand<CreatedUserDto>;
