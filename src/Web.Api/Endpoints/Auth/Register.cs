using System.Net;
using Application.Auth.Register;
using Domain.DTO.Auth;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Auth;

internal sealed class Register : IEndpoint
{
    internal sealed record Request(string Email, string FirstName, string LastName, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<CreatedUserDto> result;

            var command = new RegisterUserCommand(
            request.Email,
            request.FirstName,
            request.LastName,
            request.Password);

            result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(ApiResponse<CreatedUserDto>.Error(result.Error.ToString(), (int)HttpStatusCode.BadRequest));
            }

            return Results.Ok(ApiResponse<CreatedUserDto>.Success(result.Value, "User registered successfully"));
        }).WithTags(Tags.Auth);
    }
}
