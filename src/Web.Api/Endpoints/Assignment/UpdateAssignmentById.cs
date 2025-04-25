using System.Net;
using Application.Abstractions.Authentication;
using Application.Assignments.UpdateAssignmentById;
using Domain.DTO.Assignment;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

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
        app.MapPut("api/v1/assignment/{id:guid}", async (Guid Id, Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            Result<AssigmentResponseDto> response;
            try
            {
                var command = new UpdateAssignmentByIdCommand(Id, request.title, request.description, request.CollectionName, request.AssignmentTypeId, request.MaxScore, request.DueDate);

                response = await sender.Send(command, cancellationToken);

            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<AssigmentResponseDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Ok(ApiResponse<AssigmentResponseDto>.Success(response.Value, "assignment updated successfully"));

        }).WithTags(Tags.Assignment).RequireAuthorization()
        .AddEndpointFilter<VerifiedUserFilter>();
    }
}
