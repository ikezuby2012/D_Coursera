using Application.Abstractions.Authentication;
using Application.Assignments.DeleteAssignmentById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Assignment;

internal sealed class DeleteAssignmentById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/assignment/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteAssigmentByIdCommand(Id);

            Result result = await sender.Send(query, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.Assignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
