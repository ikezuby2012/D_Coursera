using System.Net;
using Application.Auth.Login;
using Domain.DTO.Auth;
using Domain.Users;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class Login : IEndpoint
{
    internal sealed record Request(string Email, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/login", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<LoginSuccessDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<LoginSuccessDto>.Success(value, $"User loggedin successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Auth);
    }
}
