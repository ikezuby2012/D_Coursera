using Application.Abstractions.Authentication;
using Application.Exam.CreateExamQuestions;
using Domain.DTO.Exam;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Exam;

internal sealed class AddExamQuestions : IEndpoint
{
    internal sealed class Request
    {
        public List<QuestionListDto> questions { get; set; }
        public int ExamTypeId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("exam/{Id:guid}/questions", async (Guid Id, [FromBody] Request payload, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var command = new CreateExamQuestionCommand(Id, payload.questions, payload.ExamTypeId);

            Result<CreatedExamResponseDto> result = await sender.Send(command, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<CreatedExamResponseDto>.Success(value, "Exam questions updated successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Exam).RequireAuthorization().HasRole("User");
    }
}
