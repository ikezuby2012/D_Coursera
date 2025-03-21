using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using Domain.DTO.Courses;
using Domain.Users;
using SharedKernel;

namespace Application.Courses.CreateCourse;
internal sealed class CreatedCourseCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<CreateCourseCommand, CreatedCourseDto>
{

    public async Task<Result<CreatedCourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {

        User? user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userContext.UserId, cancellationToken: cancellationToken);

        if (user is null)
        {
            return Result.Failure<CreatedCourseDto>(UserErrors.NotFound(userContext.UserId));
        }

        var course = new Course
        {
            Title = request.title.Trim(),
            Description = request.Description,
            Duration = request.Duration,
            Availability = request.Availability,
            InstructorId = userContext.UserId,
            CreatedAt = DateTime.UtcNow,
            CreatedById = !string.IsNullOrEmpty(userContext.UserId.ToString()) ? userContext.UserId.ToString() : null,
            IsSoftDeleted = false
        };

        await unitOfWork.CourseRepository.AddAsync(course);
        await unitOfWork.SaveAsync(cancellationToken);

        Course createdCourse = await unitOfWork.CourseRepository.GetAsync(x => x.Id == course.Id, includeProperties: "Instructor", cancellationToken: cancellationToken);

        var createdCourseDto = (CreatedCourseDto)createdCourse!;

        return Result.Success(createdCourseDto);
    }
}
