using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using Domain.DTO.Enrollment;
using Domain.Enrollment;
using SharedKernel;

namespace Application.Enrollment.EnrollCourse;
internal sealed class EnrollCourseCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, IApplicationDbContext context) : ICommandHandler<EnrollCourseCommand, EnrollmentSuccessResponseDto>
{
    public async Task<Result<EnrollmentSuccessResponseDto>> Handle(EnrollCourseCommand request, CancellationToken cancellationToken)
    {
        Guid userId = userContext.UserId;

        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);

        try
        {
            string lockName = $"CourseEnrollment_{request.CourseId}";

            await context.GetAppLockAsync(lockName, 5000, cancellationToken);

            Course? course = await unitOfWork.CourseRepository.SingleOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

            if (course == null)
            {
                return Result.Failure<EnrollmentSuccessResponseDto>(CourseErrors.NoCoursesFound);
            }

            bool alreadyEnrolled = await unitOfWork.EnrollmentRepository.AnyAsync(
                x => x.CourseId == request.CourseId && x.UserId == userId,
                cancellationToken);

            if (alreadyEnrolled)
            {
                return Result.Failure<EnrollmentSuccessResponseDto>(EnrollmentError.UserAlreadyEnrolled);
            }

            int enrolledCourses = await unitOfWork.EnrollmentRepository.CountAsync(c => c.CourseId == request.CourseId, cancellationToken);

            if (enrolledCourses >= course.Capacity)
            {
                return Result.Failure<EnrollmentSuccessResponseDto>(EnrollmentError.MaximumCapacity);
            }

            var enrollment = new Domain.Enrollment.Enrollment
            {
                UserId = userId,
                CourseId = course.Id,
                StatusId = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedById = userId.ToString(),
                IsSoftDeleted = false,
            };

            await unitOfWork.EnrollmentRepository.AddAsync(enrollment);
            await unitOfWork.SaveAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            var responseDto = (EnrollmentSuccessResponseDto)enrollment;

            return Result.Success(responseDto);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<EnrollmentSuccessResponseDto>(EnrollmentError.FailedToEnrollUser(ex.Message));
        }
    }
}
