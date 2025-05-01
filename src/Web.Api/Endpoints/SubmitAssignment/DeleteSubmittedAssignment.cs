
using System.Net;
using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.DeleteById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class DeleteSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/submit-assignment/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteAssignmentSubmissionByIdCommand(Id);

            try
            {
                await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<string>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<string>.Success($"{Id}", "Success!"));
        }).WithTags(Tags.SubmitAssignment)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
