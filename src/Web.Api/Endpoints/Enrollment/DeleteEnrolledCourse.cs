
using Application.Abstractions.Authentication;
using Application.Enrollment.DeleteEnrolledCourse;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Enrollment;

internal sealed class DeleteEnrolledCourse : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/enrollments/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new DeleteEnrollCourseCommand(Id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.Enrollment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
