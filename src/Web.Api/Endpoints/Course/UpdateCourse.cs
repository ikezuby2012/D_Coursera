using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.UpdateCourseById;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Course;

internal sealed class UpdateCourse : IEndpoint
{
    internal sealed record Request(Guid Id, string title, string Description, string Duration, bool Availability, double price);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/v1/course/{Id:guid}", async (Guid Id, Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<CreatedCourseDto> response;
            try
            {
                var command = new UpdateCourseByIdCommand(Id, request.title, request.Description, request.Duration, request.Availability, request.price);

                response = await sender.Send(command, cancellationToken);

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<CreatedCourseDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Ok(ApiResponse<CreatedCourseDto>.Success(response.Value, "Course updated successfully"));
        }).WithTags(Tags.Course).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
