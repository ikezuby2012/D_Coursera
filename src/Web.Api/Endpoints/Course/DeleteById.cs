
using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.DeleteCourseById;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Course;

internal sealed class DeleteById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/course/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<Guid> response;

            var command = new DeleteCommand(Id);

            try
            {
                response = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<Guid>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Ok(ApiResponse<Guid>.Success(response.Value, "Course deleted successfully"));
        }).WithTags(Tags.Course)
        .RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
