using Application.Abstractions.Authentication;
using Application.Courses.GetAllCourses;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Course;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/course", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new GetAllCourseCommand();

            Result<IEnumerable<GetAllCoursesDto>> result = await sender.Send(command, cancellationToken);

            return Results.Ok(ApiResponse<IEnumerable<GetAllCoursesDto>>.Success(result.Value, $"all results {result.Value?.ToList().Count}"));
        }).WithTags(Tags.Course).RequireAuthorization();
    }
}
