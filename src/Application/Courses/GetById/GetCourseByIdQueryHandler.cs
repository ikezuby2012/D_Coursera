using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Course;
using Domain.DTO.Courses;
using SharedKernel;

namespace Application.Courses.GetById;
internal sealed class GetCourseByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetCourseByIdQuery, CreatedCourseDto>
{
    public async Task<Result<CreatedCourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        Course course = await unitOfWork.CourseRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Instructor", cancellationToken: cancellationToken);

        if (course is null)
        {
            return Result.Failure<CreatedCourseDto>(CourseErrors.NotFound(request.Id));
        }

        var createdCourseDto = (CreatedCourseDto)course;

        return Result.Success(createdCourseDto);
    }
}
