using Application.Media.UpdateMediaById;
using Domain.DTO.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

internal sealed class UpdateMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("media/{Id:guid}", async (Guid Id, [FromQuery] Guid CourseId, IFormFileCollection Files, ISender sender, CancellationToken cancellationToken, [FromForm] string CollectionName = "") =>
        {
            var query = new UpdateMediaByIdCommand(Id, Files, CourseId, CollectionName);

            Result<UpdatedMediaDto> result;
            result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<UpdatedMediaDto>.Success(value, $"media updated successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
