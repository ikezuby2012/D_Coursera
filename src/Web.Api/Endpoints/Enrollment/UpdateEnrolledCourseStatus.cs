using Application.Abstractions.Authentication;
using Application.Enrollment.UpdateEnrollCourseStatus;
using Domain.DTO.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class UpdateEnrolledCourseStatus : IEndpoint
{
    internal sealed record Request(string Status);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/enrollments/{Id:guid}", async (Guid Id, [FromBody] Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new UpdateEnrollCourseStatusCommand(Id, request.Status);

            Result<EnrollmentSuccessResponseDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<EnrollmentSuccessResponseDto>.Success(value, $"enrolled course status updated successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Enrollment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
