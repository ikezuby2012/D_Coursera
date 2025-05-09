
using Application.Abstractions.Authentication;
using Application.Exam.CreateExam;
using Domain.DTO.Exam;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;

namespace Web.Api.Endpoints.Exam;

internal sealed class CreateExam : IEndpoint
{
    internal sealed class Request
    {
        public string Title { get; set; }
        public Guid CourseId { get; set; }
        public string Description { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal PassingMarks { get; set; }
        public string? Instructions { get; set; }
        public List<QuestionListDto> QuestionList { get; set; }
        public int ExamTypeId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/exam", async (Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateExamCommand(request.Title, request.CourseId, request.Description, request.TotalMarks, request.PassingMarks, request.Instructions, request.QuestionList, request.ExamTypeId, request.startDate, request.endDate);

            Result<CreatedExamResponseDto> result = await sender.Send(command, cancellationToken);

            return Results.Created($"/exam/{result.Value.Id}", ApiResponse<CreatedExamResponseDto>.Success(result.Value, "Exam Created Successfully"));
        }).WithTags(Tags.Exam).RequireAuthorization().HasRole("Instructor");
    }
}
