using Application.Abstractions.Authentication;
using Application.Exam.UpdateExam;
using Domain.DTO.Exam;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Exam;

internal sealed class UpdateExam : IEndpoint
{
    internal sealed class Request
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? TotalMarks { get; set; }
        public decimal? PassingMarks { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Instructions { get; set; }
        public string? Status { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("exam/{Id:guid}", async (Guid Id, [FromBody] Request payload, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new UpdateExamCommand(
                Id,
                payload.Title,
                payload.Description,
                payload.TotalMarks,
                payload.PassingMarks,
                payload.StartTime,
                payload.EndTime,
                payload.Instructions,
                payload.Status);

            Result<ExamResponseDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<ExamResponseDto>.Success(value, "exam details updated successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Exam).RequireAuthorization().HasRole("Instructor");
    }
}
