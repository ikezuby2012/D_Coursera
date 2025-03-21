
using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.CreateCourse;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

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
        app.MapPost("api/v1/course", async (Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateCourseCommand(title: request.Title, Description: request.Description, Duration: request.Duration, Availability: request.Availability);

            Result<CreatedCourseDto> result;
            try
            {
                result = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<CreatedCourseDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Created($"/courses/{result.Value.Id}", ApiResponse<CreatedCourseDto>.Success(result.Value, "Course created successfully"));
        }).WithTags(Tags.Course).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
