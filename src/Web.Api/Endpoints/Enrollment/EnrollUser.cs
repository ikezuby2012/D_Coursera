using Application.Abstractions.Authentication;
using Application.Enrollment.EnrollCourse;
using Domain.DTO.Enrollment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class EnrollUser : IEndpoint
{
    internal sealed class Request
    {
        public Guid CourseId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("enrollments", async ([FromBody] Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new EnrollCourseCommand(request.CourseId);

            Result<EnrollmentSuccessResponseDto> result = await sender.Send(command, cancellationToken).ConfigureAwait(false);

            return result.Match(value => Results.Created($"/enroll/{result.Value.Id}", ApiResponse<EnrollmentSuccessResponseDto>.Success(value, "user enrolled Successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Enrollment).RequireAuthorization().HasRole("User");
    }
}
