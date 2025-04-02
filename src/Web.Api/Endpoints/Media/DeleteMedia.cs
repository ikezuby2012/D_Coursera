using System.Net;
using Application.Abstractions.Authentication;
using Application.Media.DeleteMediaById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Media;

internal sealed class DeleteMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/media/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteMediaByIdCommand(Id);

            try
            {
                await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<string>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<string>.Success($"{Id}", "media deleted successfully for this course!"));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
