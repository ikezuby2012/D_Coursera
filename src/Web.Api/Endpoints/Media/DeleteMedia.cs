using Application.Abstractions.Authentication;
using Application.Media.DeleteMediaById;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

internal sealed class DeleteMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/v1/media/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteMediaByIdCommand(Id);

            Result result = await sender.Send(query, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
