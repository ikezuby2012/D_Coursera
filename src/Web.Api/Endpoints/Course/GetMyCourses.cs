
using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.GetMyCourses;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Course;

internal sealed class GetMyCourses : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/course/me", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<IEnumerable<CreatedCourseDto>> response;

            var command = new GetMyCoursesCommand();

            try
            {
                response = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<IEnumerable<CreatedCourseDto>>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Ok(ApiResponse<IEnumerable<CreatedCourseDto>>.Success(response.Value, "Course fetched successfully"));
        }).WithTags(Tags.Course).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
