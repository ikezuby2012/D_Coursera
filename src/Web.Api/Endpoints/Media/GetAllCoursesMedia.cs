using Application.Abstractions.Authentication;
using Application.Media.GetCourseMedia;
using Domain.DTO.Media;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Media;

internal sealed class GetAllCoursesMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/media/course/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new GetAllCourseMediaQuery(Id);

            Result<IEnumerable<PagedMediaResponseDto>> result;

            result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<IEnumerable<PagedMediaResponseDto>>.Success(value, $"media returned successfully for this course! {result.Value?.ToList().Count}")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
