using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AssignmentSubmission;
using Domain.Auth;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AssignmentSubmission.DeleteById;
internal sealed class DeleteAssignmentSubmissionByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<DeleteAssignmentSubmissionByIdCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteAssignmentSubmissionByIdCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();
        var userIdGuid = Guid.Parse(userId);

        IQueryable<AssignmentSubmissions> query = unitOfWork.AssignmentSubmissionRepository.QueryAble().Include(m => m.Assignment).Where(m => m.Id == request.Id);
        AssignmentSubmissions? assignmentSubmission = await query.FirstOrDefaultAsync(cancellationToken);

        if (assignmentSubmission == null)
        {
            return Result.Failure<Guid>(AssignmentSubmissionError.NotFound(request.Id));
        }

        /// fetch user role
        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userIdGuid, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userIdGuid));
        }

        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || assignmentSubmission.CreatedById == userId;

        if (!canUpdate)
        {
            return Result.Failure<Guid>(AuthError.NotPermitted);
        }

        assignmentSubmission.IsSoftDeleted = true;
        assignmentSubmission.UpdatedAt = DateTime.Now;
        assignmentSubmission.ModifiedBy = userId;

        unitOfWork.AssignmentSubmissionRepository.Update(assignmentSubmission);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(assignmentSubmission.Id);
    }
}
