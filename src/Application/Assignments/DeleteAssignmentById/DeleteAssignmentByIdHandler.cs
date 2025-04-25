using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Assignments;
using Domain.Auth;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Assignments.DeleteAssignmentById;
internal sealed class DeleteAssignmentByIdHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<DeleteAssigmentByIdCommand>
{
    public async Task<Result> Handle(DeleteAssigmentByIdCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();

        var userIdGuid = Guid.Parse(userId);

        IQueryable<Assignment> query = unitOfWork.AssignmentRepository.QueryAble().Include(m => m.Course).Where(m => m.Id == request.Id);
        Assignment? assignment = await query.FirstOrDefaultAsync(cancellationToken);

        if (assignment == null)
        {
            return Result.Failure(AssignmentError.NotFound(request.Id));
        }

        /// fetch user role
        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userIdGuid, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(userIdGuid));
        }

        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || assignment.CreatedById == userId;

        if (!canUpdate)
        {
            return Result.Failure(AuthError.NotPermitted);
        }


        assignment.IsSoftDeleted = true;
        assignment.UpdatedAt = DateTime.Now;
        assignment.ModifiedBy = userId;

        unitOfWork.AssignmentRepository.Update(assignment);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
