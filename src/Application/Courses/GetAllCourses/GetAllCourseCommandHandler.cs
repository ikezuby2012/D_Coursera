using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using Domain.DTO.Courses;
using SharedKernel;

namespace Application.Courses.GetAllCourses;
internal sealed class GetAllCourseCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetAllCourseCommand, IEnumerable<GetAllCoursesDto>>
{
    public async Task<Result<IEnumerable<GetAllCoursesDto>>> Handle(GetAllCourseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Course> courses = await unitOfWork.CourseRepository.GetAllAsync(cancellationToken);

            if (courses == null)
            {
                return Result.Failure<IEnumerable<GetAllCoursesDto>>(CourseErrors.NoCoursesFound);
            }

            var courseDtos = courses.Select(course => (GetAllCoursesDto)course).ToList();

            return Result.Success<IEnumerable<GetAllCoursesDto>>(courseDtos);
        }
        catch (Exception ex)
        {
            return Result.Failure<IEnumerable<GetAllCoursesDto>>(CourseErrors.FailedToFetchCourses(ex.Message));
        }
    }
}
