using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;
using SharedKernel;

using Assignment = Domain.Assignments.Assignment;

namespace Application.Assignments.CreateAssignment;
internal sealed class CreateAssignmentCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<CreateAssigmentCommand, CreatedAssignmentDto>
{
    public async Task<Result<CreatedAssignmentDto>> Handle(CreateAssigmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = new Assignment
        {
            Title = request.title.Trim(),
            Description = request.description.Trim(),
            CourseId = Guid.Parse(request.CourseId),
            CollectionName = request.CollectionName,
            MaxScore = request.MaxScore,
            AssignmentTypeId = request.Type,
            DueDate = request.DueDate,
            CreatedAt = DateTime.Now,
            CreatedById = !string.IsNullOrEmpty(userContext.UserId.ToString()) ? userContext.UserId.ToString() : null,
            IsSoftDeleted = false
        };


        await unitOfWork.AssignmentRepository.AddAsync(assignment);
        await unitOfWork.SaveAsync(cancellationToken);

        Assignment? createdAssignment = await unitOfWork.AssignmentRepository.GetAsync(x => x.Id == assignment.Id, includeProperties: "Course", cancellationToken: cancellationToken);

        var createdAssignmentDto = (CreatedAssignmentDto)createdAssignment!;

        return Result.Success(createdAssignmentDto);
    }
}
