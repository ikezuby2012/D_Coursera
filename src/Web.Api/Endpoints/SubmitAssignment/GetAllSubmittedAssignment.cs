using System.Net;
using Application.AssignmentSubmission.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class GetAllSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/submit-assignment", async (
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

            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<GetAllAssignmentSubmissionResponse>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }
            return Results.Ok(ApiResponse<GetAllAssignmentSubmissionResponse>.Success(result.Value, "all Submitted data returned successfully!"));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
