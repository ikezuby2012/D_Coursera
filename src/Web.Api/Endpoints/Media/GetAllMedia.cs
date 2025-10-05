using Application.Media.GetAllMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

internal sealed class GetAllMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("media", async (
            ISender sender, CancellationToken cancellationToken,
            [FromQuery] int pageSize = 1000,
            [FromQuery] int pageNumber = 0,
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null,
            [FromQuery] Guid? courseId = null,
            [FromQuery] string? collectionName = null) =>
        {
            var query = new GetMediaQuery(
               PageSize: pageSize,
               pageNumber: pageNumber,
               DateFrom: dateFrom,
               DateTo: dateTo,
               CourseId: courseId,
               CollectionName: collectionName);

            Result<GetAllMediaResponse> result;

            result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<GetAllMediaResponse>.Success(value, $"media returned successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
