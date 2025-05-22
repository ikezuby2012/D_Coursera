using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auth;
using Domain.Users;
using SharedKernel;

namespace Application.Enrollment.DeleteEnrolledCourse;
internal sealed class DeleteEnrollCourseCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<DeleteEnrollCourseCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteEnrollCourseCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        Domain.Enrollment.Enrollment? enrollCred = await unitOfWork.EnrollmentRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (enrollCred == null)
        {
            return Result.Failure<Guid>(Domain.Enrollment.EnrollmentError.NotFound(request.Id));
        }

        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userId, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(userId));
        }
        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || enrollCred.CreatedById == userId.ToString();

        if (!canUpdate)
        {
            return Result.Failure<Guid>(AuthError.NotPermitted);
        }

        enrollCred.IsSoftDeleted = true;
        enrollCred.UpdatedAt = DateTime.UtcNow;
        enrollCred.ModifiedBy = userId.ToString();

        unitOfWork.EnrollmentRepository.Update(enrollCred);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success(enrollCred.Id);
    }
}
