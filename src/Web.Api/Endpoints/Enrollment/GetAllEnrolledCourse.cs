using Application.Enrollment.GetAllEnrollCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class GetAllEnrolledCourse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("enrollments", async (
             ISender sender, CancellationToken cancellationToken,
            [FromQuery] int pageSize = 1000,
            [FromQuery] int pageNumber = 0,
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null,
            [FromQuery] Guid? courseId = null
            ) =>
        {
            var query = new GetAllEnrollCourseQuery(
            PageSize: pageSize,
            pageNumber: pageNumber,
            DateFrom: dateFrom,
            DateTo: dateTo,
            CourseId: courseId);

            Result<GetAllEnrollCoursesResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<GetAllEnrollCoursesResponse>.Success(value, $"all enrolled courses returned successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Enrollment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
