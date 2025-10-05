using Application.AssignmentSubmission.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class GetAllSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("submit-assignment", async (
            ISender sender, CancellationToken cancellationToken,
            [FromQuery] int PageSize = 1000,
            [FromQuery] int pageNumber = 0,
            [FromQuery] DateTime? DateFrom = null,
            [FromQuery] DateTime? DateTo = null,
            [FromQuery] Guid? assignmentId = null) =>
        {
            var query = new GetAllAssignmentSubmissionQuery(
                PageSize: PageSize,
                pageNumber: pageNumber,
                DateFrom: DateFrom,
                DateTo: DateTo,
                assignmentId: assignmentId);

            Result<GetAllAssignmentSubmissionResponse> result;

            result = await sender.Send(query, cancellationToken);
            return result.Match(value => Results.Ok(ApiResponse<GetAllAssignmentSubmissionResponse>.Success(value, $"all Submitted data returned successfully!")), error => CustomResults.Problem(error));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
