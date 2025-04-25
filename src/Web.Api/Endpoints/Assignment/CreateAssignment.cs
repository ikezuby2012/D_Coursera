using System.Net;
using Application.Abstractions.Authentication;
using Application.Assignments.CreateAssignment;
using Domain.DTO.Assignment;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;

namespace Web.Api.Endpoints.Assignment;

internal sealed class CreateAssignment : IEndpoint
{
    internal sealed class Request
    {
        public string title { get; set; }
        public string description { get; set; }
        public string CourseId { get; set; }
        public string? CollectionName { get; set; }
        public double MaxScore { get; set; }
        public int AssignmentTypeId { get; set; }
        public DateTime DueDate { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/assignment", async (Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateAssigmentCommand(request.title, request.description, request.CollectionName, request.CourseId, request.DueDate, request.MaxScore, request.AssignmentTypeId);

            Result<CreatedAssignmentDto> result;
            try
            {
                result = await sender.Send(command, cancellationToken);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ApiResponse<CreatedAssignmentDto>.Error(ex.Message.ToString(), (int)HttpStatusCode.BadRequest));
            }
            return Results.Created($"/assignment/{result.Value.Id}", ApiResponse<CreatedAssignmentDto>.Success(result.Value, "Assignment created successfully"));
        }).WithTags(Tags.Assignment).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
