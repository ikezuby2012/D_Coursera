
using Application.Abstractions.Authentication;
using Application.Exam.GetExamQuestions;
using Domain.DTO.Exam;
using MediatR;
using SharedKernel;
using Web.Api.EndpointFilter;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Exam;

internal sealed class GetExamQuestions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("exam/{id:guid}/questions", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new GetExamQuestionQuery(Id);

            Result<ExamQuestionsDto> result = await sender.Send(query, cancellationToken);

            return result.Match(value => Results.Ok(ApiResponse<ExamQuestionsDto>.Success(value, "Exam questions fetched successfully")), error => CustomResults.Problem(error));
        }).WithTags(Tags.Exam).RequireAuthorization().AddEndpointFilter<VerifiedUserFilter>();
    }
}
