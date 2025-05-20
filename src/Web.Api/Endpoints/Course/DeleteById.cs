
using System.Net;
using Application.Abstractions.Authentication;
using Application.Courses.DeleteCourseById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Course;

internal sealed class DeleteById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/course/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new DeleteCommand(Id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.Course)
        .RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
