using System.Net;
using Application.Abstractions.Authentication;
using Application.Media.GetCourseMedia;
using Domain.DTO.Media;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Media;

internal sealed class GetAllCoursesMedia : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/v1/media/course/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new GetAllCourseMediaQuery(Id);

            Result<IEnumerable<PagedMediaResponseDto>> result;
            try
            {
                result = await sender.Send(query, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<IEnumerable<PagedMediaResponseDto>>.Error(ex.Message.ToString(), (int)HttpStatusCode.InternalServerError));
            }

            return Results.Ok(ApiResponse<IEnumerable<PagedMediaResponseDto>>.Success(result.Value, "media returned successfully for this course!"));
        }).WithTags(Tags.Media)
        .RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
