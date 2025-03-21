using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.GetById;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;

namespace Web.Api.Endpoints.Course;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/course/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<CreatedCourseDto> response;
            try
            {
                var command = new GetCourseByIdQuery(Id);

                response = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<CreatedCourseDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Ok(ApiResponse<CreatedCourseDto>.Success(response.Value, "Course fetched successfully"));
        }).WithTags(Tags.Course).RequireAuthorization();
    }
}
