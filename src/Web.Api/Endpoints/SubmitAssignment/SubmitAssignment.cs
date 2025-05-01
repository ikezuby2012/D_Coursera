using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.Create;
using Domain.Course;
using Domain.DTO.AssignmentSubmission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class SubmitAssignment : IEndpoint
{
    // Guid assignmentId, string submissionText, IFormFile file, string? feedback
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/submit-assignment", async (
            [FromForm] Guid AssignmentId, [FromForm] string submissionText, [FromForm] string feedback, ISender sender, IUserContext userContext, CancellationToken cancellationToken, IFormFile? file
            ) =>
        {
            var command = new CreateAssignmentSubmissionCommand(AssignmentId, submissionText, file, feedback);

            Result<CreatedAssignmentSubmissionDto> result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new
                {
                    Error = "failed to submit assignment",
                    Details = result.Error
                });
            }

            return Results.Created($"/submit-assignment/{AssignmentId}", ApiResponse<CreatedAssignmentSubmissionDto>.Success(result.Value, "you have successfully submitted your assignment"));
        }).WithTags(Tags.SubmitAssignment);
    }
}
