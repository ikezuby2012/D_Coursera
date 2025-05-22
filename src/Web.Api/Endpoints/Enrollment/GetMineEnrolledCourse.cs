using Application.Abstractions.Authentication;
using Application.Enrollment.GetAllEnrollCourse;
using Application.Enrollment.GetMineEnrollCourse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class GetMineEnrolledCourse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/enrollments/me", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken,
            [FromQuery] int pageSize = 1000,
            [FromQuery] int pageNumber = 0,
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null) =>
        {
            var query = new GetMineEnrollCoursesQuery(pageSize, pageNumber, dateFrom, dateTo);

            Result<GetAllEnrollCoursesResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<GetAllEnrollCoursesResponse>.Success(value, $"my enrolled courses returned successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Enrollment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
