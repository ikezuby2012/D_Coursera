using Application.Abstractions.Authentication;
using Application.Courses.GetAllCourses;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/course", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new GetAllCourseCommand();

            Result<IEnumerable<GetAllCoursesDto>> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<IEnumerable<GetAllCoursesDto>>.Success(value, $"all results {result.Value?.ToList().Count}")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization();
    }
}
