using System.Net;
using Application.Media.UpdateMediaById;
using Domain.DTO.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Media;

internal sealed class UpdateMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("api/v1/media/{Id:guid}", async (Guid Id, [FromQuery] Guid CourseId, IFormFileCollection Files, ISender sender, CancellationToken cancellationToken, [FromForm] string CollectionName = "") =>
        {
            var query = new UpdateMediaByIdCommand(Id, Files, CourseId, CollectionName);

            Result<UpdatedMediaDto> result;

            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<UpdatedMediaDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<UpdatedMediaDto>.Success(result.Value, "Media updated successfully"));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
