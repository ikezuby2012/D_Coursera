using Application.Abstractions.Authentication;
using Application.Courses.CreateCourse;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class Create : IEndpoint
{
    internal sealed class Request
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public bool Availability { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("course", async (Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateCourseCommand(title: request.Title, Description: request.Description, Duration: request.Duration, Availability: request.Availability);

            Result<CreatedCourseDto> result;

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Created($"/courses/{result.Value.Id}", ApiResponse<CreatedCourseDto>.Success(value, "Course created successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization().HasRole("Instructor");

    }
}
