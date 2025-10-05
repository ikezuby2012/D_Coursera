using Application.Abstractions.Authentication;
using Application.Exam.AutoGradeExamSubmission;
using Domain.DTO.Exam;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Exam;

internal sealed class AutoGradeExam : IEndpoint
{
    internal sealed class Request
    {
        public DateTime StartTime { get; set; } /// when candidate start the exam
        public DateTime EndTime { get; set; }
        public List<ExamSubmissionRequestDto> AnswersScripts { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("exam/auto-submission/{Id:guid}", async (Guid Id, [FromBody] Request request, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new AutoGradeExamSubmissionCommand(Id, request.StartTime, request.EndTime, request.AnswersScripts);

            Result<AutoGradeExamSubmissionResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<AutoGradeExamSubmissionResponse>.Success(value, "Answers has been marked successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Exam).RequireAuthorization().HasRole("User");
    }
}
