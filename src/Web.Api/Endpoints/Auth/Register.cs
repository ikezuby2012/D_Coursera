using Application.Auth.Register;
using Domain.DTO.Auth;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class Register : IEndpoint
{
    internal sealed record Request(string Email, string FirstName, string LastName, string Password, int RoleId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<CreatedUserDto> result;

            var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password, request.RoleId);

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<CreatedUserDto>.Success(value, $"User registered successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Auth);
    }
}
