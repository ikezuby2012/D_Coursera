using Application.Abstractions.Authentication;
using Application.Assignments.GetAllAssignment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Assignment;

internal sealed class GetAllAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("assignment", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken,
           [FromQuery] int pageSize = 1000,
           [FromQuery] int pageNumber = 1,
           [FromQuery] DateTime? dateFrom = null,
           [FromQuery] DateTime? dateTo = null,
           [FromQuery] Guid? courseId = null,
           [FromQuery] string? collectionName = null
           ) =>
        {
            var query = new GetAllAssignmentQuery(
               PageSize: pageSize,
               pageNumber: pageNumber,
               DateFrom: dateFrom,
               DateTo: dateTo,
               CourseId: courseId,
               CollectionName: collectionName);

            Result<GetAllAssignmentResponse> result;

            result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<GetAllAssignmentResponse>.Success(value, $"All Assignments returned successfully!")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Assignment).RequireAuthorization()
       .AddEndpointFilter<VerifiedUserFilter>();
    }
}
