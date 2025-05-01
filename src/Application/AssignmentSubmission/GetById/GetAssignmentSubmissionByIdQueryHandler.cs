using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.AssignmentSubmission;
using SharedKernel;

namespace Application.AssignmentSubmission.GetById;
internal class GetAssignmentSubmissionByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAssignmentSubmissionByIdQuery, AssignmentSubmissionResponseDto>
{
    public async Task<Result<AssignmentSubmissionResponseDto>> Handle(GetAssignmentSubmissionByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.AssignmentSubmission.AssignmentSubmissions assignmentSubmissions = await unitOfWork.AssignmentSubmissionRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Assignment", cancellationToken: cancellationToken);

        if (assignmentSubmissions is null)
        {
            return Result.Failure<AssignmentSubmissionResponseDto>(Domain.AssignmentSubmission.AssignmentSubmissionError.InvalidAssignmentId(request.Id.ToString()));
        }

        var assignmentSubmissionDto = (AssignmentSubmissionResponseDto)assignmentSubmissions;
        return Result.Success(assignmentSubmissionDto);
    }
}
