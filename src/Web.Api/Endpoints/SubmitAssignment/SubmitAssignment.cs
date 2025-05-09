using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.Create;
using Domain.DTO.AssignmentSubmission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class SubmitAssignment : IEndpoint
{
    // Guid assignmentId, string submissionText, IFormFile file, string? feedback
    internal sealed class Request
    {
        public Guid AssignmentId { get; set; }
        public string SubmissionText { get; set; }
        public string Feedback { get; set; }
        public IFormFile? File { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/submit-assignment", async (
            [FromForm] Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken
            ) =>
        {
            var command = new CreateAssignmentSubmissionCommand(request.AssignmentId, request.SubmissionText, request.File, request.Feedback);

            Result<CreatedAssignmentSubmissionDto> result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new
                {
                    Error = "failed to submit assignment",
                    Details = result.Error
                });
            }

            return Results.Created($"/submit-assignment/{request.AssignmentId}", ApiResponse<CreatedAssignmentSubmissionDto>.Success(result.Value, "you have successfully submitted your assignment"));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>().DisableAntiforgery();
    }
}
