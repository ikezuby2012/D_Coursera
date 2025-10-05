using Application.Abstractions.Authentication;
using Application.Courses.GetMyCourses;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class GetMyCourses : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("course/me", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new GetMyCoursesCommand();

            Result<IEnumerable<CreatedCourseDto>> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<IEnumerable<CreatedCourseDto>>.Success(value, $"all results {result.Value?.ToList().Count}")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
