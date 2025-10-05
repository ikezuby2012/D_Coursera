using Application.Abstractions.Authentication;
using Application.Exam.DeleteExam;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Exam;

internal sealed class DeleteExam : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("exam/{id:guid}", async (Guid Id, ISender sender, IUserContext userContext, CancellationToken cancellationToken) =>
        {
            var query = new DeleteExamCommand(Id);

            Result result = await sender.Send(query, cancellationToken);

            return result.Match(() => Results.NoContent(), error => CustomResults.Problem(result));
        }).WithTags(Tags.Exam).RequireAuthorization().HasRole("Instructor");
    }
}
