using System.Net;
using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.GetById;
using Domain.DTO.AssignmentSubmission;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class GetSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/submit-assignment/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new GetAssignmentSubmissionByIdQuery(Id);

            Result<AssignmentSubmissionResponseDto> result;
            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<AssignmentSubmissionResponseDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<AssignmentSubmissionResponseDto>.Success(result.Value, "data submitted for the assignemnt!"));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
