using Application.Abstractions.Authentication;
using Application.Courses.UpdateCourseById;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class UpdateCourse : IEndpoint
{
    internal sealed record Request(Guid Id, string title, string Description, string Duration, bool Availability, double price);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("course/{Id:guid}", async (Guid Id, Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new UpdateCourseByIdCommand(Id, request.title, request.Description, request.Duration, request.Availability, request.price);

            Result<CreatedCourseDto> result = await sender.Send(command, cancellationToken);

            // return Results.Ok(ApiResponse<CreatedCourseDto>.Success(response.Value, "Course updated successfully"));
            return result.Match(value => Results.Ok(ApiResponse<CreatedCourseDto>.Success(value, "Course fetched successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
