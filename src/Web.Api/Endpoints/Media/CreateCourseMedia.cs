using Application.Abstractions.Authentication;
using Application.Media.CreateMedia;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Media;

internal sealed class CreateCourseMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/media/course",
            async (IFormFileCollection Files, [FromForm] string CourseId, [FromForm] string CollectionName, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            if (!Guid.TryParse(CourseId, out Guid courseId))
            {
                return Results.BadRequest(new { Error = "Invalid CourseId format. Must be a valid GUID." });
            }

            var command = new CreateMediaCommand(Files, courseId, CollectionName);

            Result<IEnumerable<Domain.DTO.Media.CreatedMediaDto>> result = await sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new
                {
                    Error = "failed to upload data",
                    Details = result.Error
                });
            }
            return Results.Created($"/media/{CourseId}", ApiResponse<IEnumerable<Domain.DTO.Media.CreatedMediaDto>>.Success(result.Value, "media created for the course successfully"));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>().DisableAntiforgery();
        //.WithMetadata(new IgnoreAntiforgeryTokenAttribute());
    }
}
