using Application.Auth.ResendOTP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Auth;

internal sealed class ResendOtp : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/resend-otp", async ([FromQuery] string email, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ResendOtpCommand(email);

            Result<Guid> result = await sender.Send(command, cancellationToken);


            return result.Match(value => Results.Ok(ApiResponse<Guid>.Success(value, $"OTP sent to user mail box successfully")), error => CustomResults.Problem(error));

        }).WithTags(Tags.Auth);
    }
}
