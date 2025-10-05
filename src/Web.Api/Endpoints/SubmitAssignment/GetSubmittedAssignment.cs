using Application.Abstractions.Authentication;
using Application.AssignmentSubmission.GetById;
using Domain.DTO.AssignmentSubmission;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SubmitAssignment;

internal sealed class GetSubmittedAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("submit-assignment/{Id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new GetAssignmentSubmissionByIdQuery(Id);

            Result<AssignmentSubmissionResponseDto> result;

            result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<AssignmentSubmissionResponseDto>.Success(value, $"data submitted for the assignemnt!")), error => CustomResults.Problem(error));
        }).WithTags(Tags.SubmitAssignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
