using Application.Abstractions.Authentication;
using Application.Courses.GetById;
using Domain.DTO.Courses;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("course/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<CreatedCourseDto> result;

            var command = new GetCourseByIdQuery(Id);

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<CreatedCourseDto>.Success(value, "Course fetched successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Course).RequireAuthorization();
    }
}
