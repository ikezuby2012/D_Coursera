using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auth;
using Domain.DTO.Enrollment;
using Domain.Enrollment;
using Domain.Users;
using SharedKernel;

namespace Application.Enrollment.UpdateEnrollCourseStatus;
internal sealed class UpdateEnrollCourseStatusCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<UpdateEnrollCourseStatusCommand, EnrollmentSuccessResponseDto>
{
    public async Task<Result<EnrollmentSuccessResponseDto>> Handle(UpdateEnrollCourseStatusCommand request, CancellationToken cancellationToken)
    {
        Domain.Enrollment.Enrollment? enrollCred = await unitOfWork.EnrollmentRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (enrollCred == null)
        {
            return Result.Failure<EnrollmentSuccessResponseDto>(EnrollmentError.NotFound(request.Id));
        }

        Guid userId = userContext.UserId;

        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userId, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure<EnrollmentSuccessResponseDto>(UserErrors.NotFound(userId));
        }
        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || enrollCred.UserId == userContext.UserId;

        if (!canUpdate)
        {
            return Result.Failure<EnrollmentSuccessResponseDto>(AuthError.NotPermitted);
        }

        enrollCred.StatusId = EnrollmentStatus.FromName(request.status ?? "Pending")!.Id;
        enrollCred.UpdatedAt = DateTime.UtcNow;
        enrollCred.ModifiedBy = userId.ToString();

        unitOfWork.EnrollmentRepository.Update(enrollCred);
        await unitOfWork.SaveAsync(cancellationToken);

        var response = (EnrollmentSuccessResponseDto)enrollCred;
        return Result.Success(response);
    }
}
