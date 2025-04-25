using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Assignments;
using Domain.Auth;
using Domain.DTO.Assignment;
using Domain.DTO.Media;
using Domain.Media;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Serilog.Parsing;
using SharedKernel;

using Assignment = Domain.Assignments.Assignment;

namespace Application.Assignments.UpdateAssignmentById;
internal sealed class UpdateAssignmentByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<UpdateAssignmentByIdCommand, AssigmentResponseDto>
{
    public async Task<Result<AssigmentResponseDto>> Handle(UpdateAssignmentByIdCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();

        var userIdGuid = Guid.Parse(userId);

        IQueryable<Assignment> query = unitOfWork.AssignmentRepository.QueryAble().Include(m => m.Course).Where(m => m.Id == request.id);
        Assignment? assignment = await query.FirstOrDefaultAsync(cancellationToken);

        if (assignment == null)
        {
            return Result.Failure<AssigmentResponseDto>(AssignmentError.NotFound(request.id));
        }

        /// fetch user role
        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userIdGuid, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure<AssigmentResponseDto>(UserErrors.NotFound(userIdGuid));
        }

        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || assignment.CreatedById == userId;

        if (!canUpdate)
        {
            return Result.Failure<AssigmentResponseDto>(AuthError.NotPermitted);
        }

        if (request.Title != null)
        {
            assignment.Title = request.Title;
        }
        if (request.Description != null)
        {
            assignment.Description = request.Description;
        }
        if (request.CollectionName != null)
        {
            assignment.CollectionName = request.CollectionName;
        }
        if (request.AssignmentType.HasValue)
        {
            assignment.AssignmentTypeId = request.AssignmentType.Value;
        }
        if (request.MaxScore.HasValue)
        {
            assignment.MaxScore = request.MaxScore.Value;
        }
        if (request.DueDate.HasValue)
        {
            assignment.DueDate = request.DueDate.Value;
        }

        assignment.UpdatedAt = DateTime.Now;
        assignment.ModifiedBy = userId;

        unitOfWork.AssignmentRepository.Update(assignment);
        await unitOfWork.SaveAsync(cancellationToken);

        var updatedAssignedDto = (AssigmentResponseDto)assignment!;

        return Result.Success(updatedAssignedDto);
    }
}
