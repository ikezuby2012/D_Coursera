using System.Net;
using Application.Media.GetAllMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Media;

internal sealed class GetAllMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/media", async (
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

            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<GetAllMediaResponse>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }
            return Results.Ok(ApiResponse<GetAllMediaResponse>.Success(result.Value, "media returned successfully!"));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
