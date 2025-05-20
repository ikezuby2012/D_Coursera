using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.DeleteById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class DeleteSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/submit-assignment/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteAssignmentSubmissionByIdCommand(Id);

            Result result = await sender.Send(query, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.SubmitAssignment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
