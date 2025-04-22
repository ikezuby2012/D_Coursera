using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;
using SharedKernel;

using Assignment = Domain.Assignments.Assignment;

namespace Application.Assignments.GetAssignmentById;
internal sealed class GetAssignmentByIdQueryHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetAssignmentByIdQuery, CreatedAssignmentDto>
{
    public async Task<Result<CreatedAssignmentDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        Assignment? assignment = await unitOfWork.AssignmentRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Course", cancellationToken: cancellationToken);

        if (assignment is null)
        {
            return Result.Failure<CreatedAssignmentDto>(Domain.Assignments.AssignmentError.InvalidCourseId(request.Id.ToString()));
        }

        var assignmentDto = (CreatedAssignmentDto)assignment;
        return Result.Success(assignmentDto);
    }
}
