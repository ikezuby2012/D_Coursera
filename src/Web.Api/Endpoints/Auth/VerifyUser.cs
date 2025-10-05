using Application.Auth.VerifyUser;
using Domain.DTO.Auth;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class VerifyUser : IEndpoint
{
    internal sealed record Request(string Email, string Otp);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/verify-user", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new VerifyUserCommand(request.Email, request.Otp);
            Result<CreatedUserDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<CreatedUserDto>.Success(value, $"User verified successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Auth);
    }
}
