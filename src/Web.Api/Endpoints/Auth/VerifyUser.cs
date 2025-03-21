using System.Net;
using Application.Auth.VerifyUser;
using Domain.DTO.Auth;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Auth;

internal sealed class VerifyUser : IEndpoint
{
    internal sealed record Request(string Email, string Otp);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/verify-user", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new VerifyUserCommand(request.Email, request.Otp);
            Result<CreatedUserDto> result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(ApiResponse<CreatedUserDto>.Error(result.Error.ToString(), (int)HttpStatusCode.BadRequest));
            }

            return Results.Ok(ApiResponse<CreatedUserDto>.Success(result.Value, "User verified successfully"));
        }).WithTags(Tags.Auth);
    }
}
