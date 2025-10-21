using Application.Abstractions.Authentication;
using Application.Courses.CreateCourse;
using Domain.DTO.Courses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public string Category { get; set; }
        public string CourseLevel { get; set; }
        public string Language { get; set; }
        public string? TimeZone { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("course", async (
            IFormFileCollection Files,
            [FromForm] Request request,
            ISender sender,
            IUserContext userContext,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateCourseCommand(title: request.Title, Description: request.Description, Duration: request.Duration, Availability: request.Availability, request.Category, request.CourseLevel, request.Language, request.TimeZone ?? "UTC+1", request.StartDate, request.EndDate, Files);

            Result<CreatedCourseDto> result;

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Created($"/courses/{result.Value.Id}", ApiResponse<CreatedCourseDto>.Success(value, "Course created successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization().HasRole(Domain.UserRole.UserRoles.Instructor.Name).DisableAntiforgery();

    }
}
