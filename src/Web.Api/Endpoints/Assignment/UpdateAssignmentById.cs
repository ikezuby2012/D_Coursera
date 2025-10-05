using Application.Abstractions.Authentication;
using Application.Assignments.UpdateAssignmentById;
using Domain.DTO.Assignment;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Assignment;

internal sealed class UpdateAssignmentById : IEndpoint
{
    internal sealed class Request
    {
        public string? title { get; set; }
        public string? description { get; set; }
        public string? CourseId { get; set; }
        public string? CollectionName { get; set; }
        public double MaxScore { get; set; }
        public int AssignmentTypeId { get; set; }
        public DateTime DueDate { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("assignment/{id:guid}", async (Guid Id, Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<AssigmentResponseDto> result;

            var command = new UpdateAssignmentByIdCommand(Id, request.title, request.description, request.CollectionName, request.AssignmentTypeId, request.MaxScore, request.DueDate);

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<AssigmentResponseDto>.Success(value, $"assignment updated successfully!")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Assignment).RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
