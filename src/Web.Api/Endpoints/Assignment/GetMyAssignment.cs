using System.Net;
using Application.Abstractions.Authentication;
using Application.Assignments.GetAllAssignment;
using Application.Assignments.GetMyAssignment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Assignment;

internal sealed class GetMyAssignment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/assignment/me", async (ISender sender, IUserContext userContext, CancellationToken cancellationToken,
            [FromQuery] int pageSize = 1000,
            [FromQuery] int pageNumber = 0,
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null,
            [FromQuery] Guid? courseId = null,
            [FromQuery] string? collectionName = null
            ) =>
        {
            var query = new GetMyAssignmentQuery(
               PageSize: pageSize,
               pageNumber: pageNumber,
               DateFrom: dateFrom,
               DateTo: dateTo,
               CourseId: courseId,
               CollectionName: collectionName);

            Result<GetAllAssignmentResponse> result;

            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<GetAllAssignmentResponse>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }
            return Results.Ok(ApiResponse<GetAllAssignmentResponse>.Success(result.Value, "All my Assignments returned successfully!"));

        }).WithTags(Tags.Assignment).RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
