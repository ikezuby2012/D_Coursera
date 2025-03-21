using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using SharedKernel;

namespace Application.Courses.DeleteCourseById;
internal sealed class DeleteCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<DeleteCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        Course? course = await unitOfWork.CourseRepository.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (course is null)
        {
            return Result.Failure<Guid>(CourseErrors.NotFound(request.Id));
        }

        course.IsSoftDeleted = true;
        course.ModifiedBy = userContext.UserId.ToString();
        course.UpdatedAt = DateTime.UtcNow;

        unitOfWork.CourseRepository.Update(course);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success<Guid>(course.Id);
    }
}
