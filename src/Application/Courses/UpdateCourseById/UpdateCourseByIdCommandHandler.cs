using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using Domain.DTO.Courses;
using SharedKernel;

namespace Application.Courses.UpdateCourseById;
internal sealed class UpdateCourseByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<UpdateCourseByIdCommand, CreatedCourseDto>
{
    public async Task<Result<CreatedCourseDto>> Handle(UpdateCourseByIdCommand request, CancellationToken cancellationToken)
    {
        Course? course = await unitOfWork.CourseRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Instructor", true, cancellationToken);

        if (course is null)
        {
            return Result.Failure<CreatedCourseDto>(CourseErrors.NoCoursesFound);
        }

        if (!string.IsNullOrEmpty(request.title))
        {
            course.Title = request.title;
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            course.Description = request.Description;
        }
        if (!string.IsNullOrEmpty(request.Duration))
        {
            course.Duration = request.Duration;
        }
        if (!string.IsNullOrEmpty(request.Availability.ToString()))
        {
            course.Availability = request.Availability;
        }

        course.ModifiedBy = userContext.UserId.ToString();
        course.UpdatedAt = DateTime.UtcNow;

        unitOfWork.CourseRepository.Update(course);
        await unitOfWork.SaveAsync(cancellationToken);

        var createdCourseDto = (CreatedCourseDto)course!;

        return Result.Success(createdCourseDto);
    }
}
