using System.Net;
using Application.Auth.ResendOTP;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.Api.Endpoints.Auth;

internal sealed class ResendOtp : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/auth/resend-otp", async ([FromQuery] string email, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ResendOtpCommand(email);

            Result<Guid> result = await sender.Send(command, cancellationToken);


            if (!result.IsSuccess)
            {
                return Results.BadRequest(ApiResponse<Guid>.Error(result.Error.ToString(), (int)HttpStatusCode.BadRequest));
            }

            return Results.Ok(ApiResponse<Guid>.Success(result.Value, "OTP sent to user mail box successfully"));
        }).WithTags(Tags.Auth);
    }
}
