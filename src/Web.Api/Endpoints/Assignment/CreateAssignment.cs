using Application.Abstractions.Authentication;
using Application.Assignments.CreateAssignment;
using Domain.DTO.Assignment;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

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
        app.MapPost("assignment", async (Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateAssigmentCommand(request.title, request.description, request.CollectionName, request.CourseId, request.DueDate, request.MaxScore, request.AssignmentTypeId);

            Result<CreatedAssignmentDto> result;

            result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Created($"/assignment/{result.Value.Id}", ApiResponse<CreatedAssignmentDto>.Success(value, "Assignment created successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Assignment).RequireAuthorization().HasRole("Instructor");
    }
}
