using System.Net;
using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.UpdateById;
using Domain.DTO.AssignmentSubmission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class UpdateSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/submit-assignment/{Id:guid}", async (
            Guid Id, [FromForm] Guid AssignmentId, [FromForm] string submissionText, [FromForm] string feedback, ISender sender, IUserContext userContext, CancellationToken cancellationToken, IFormFile? file) =>
        {
            var command = new UpdateAssignmentSubmissionByIdCommand(Id, AssignmentId, submissionText, file, feedback);

            Result<UpdatedAssignmentSubmissionDto> result;

            try
            {
                result = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<UpdatedAssignmentSubmissionDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<UpdatedAssignmentSubmissionDto>.Success(result.Value, "you have successfully updated the data submitted to this assignment"));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
