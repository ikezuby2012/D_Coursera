using System.Net;
using Application.Auth.Login;
using Domain.DTO.Auth;
using Domain.Users;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Auth;

internal sealed class Login : IEndpoint
{
    internal sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/auth/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<LoginSuccessDto> result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(ApiResponse<LoginSuccessDto>.Error(result.Error.ToString(), (int)HttpStatusCode.BadRequest));
            }

            return Results.Ok(ApiResponse<LoginSuccessDto>.Success(result.Value, "User loggedin successfully"));
        }).WithTags(Tags.Auth);
    }
}
