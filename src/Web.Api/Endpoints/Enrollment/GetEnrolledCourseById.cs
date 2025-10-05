using Application.Abstractions.Authentication;
using Application.Enrollment.GetEnrolledCourseById;
using Domain.DTO.Enrollment;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class GetEnrolledCourseById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("enrollments/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new GetEnrolledCourseByIdQuery(Id);

            Result<EnrollmentSuccessResponseDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<EnrollmentSuccessResponseDto>.Success(value, $"enrolled course returned successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Enrollment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
