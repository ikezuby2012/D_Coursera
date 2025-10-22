using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auth;
using Domain.Course;
using Domain.DTO.Courses;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Courses.GetMyCourses;
internal sealed class GetMyCoursesCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<GetMyCoursesCommand, IEnumerable<CreatedCourseDto>>
{
    public async Task<Result<IEnumerable<CreatedCourseDto>>> Handle(GetMyCoursesCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();

        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure<IEnumerable<CreatedCourseDto>>(AuthError.NotAuthenticated);
        }

        var userIdGuid = Guid.Parse(userId);
        IEnumerable<Course> courses = await unitOfWork.CourseRepository.QueryAble().Include(x => x.TimelineMedias).Where(x => x.InstructorId == userIdGuid).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);//.FindAsync(x => x.InstructorId == userIdGuid, cancellationToken: cancellationToken);

        var myCourses = courses.Select(x => (CreatedCourseDto)x).ToList();

        return Result.Success<IEnumerable<CreatedCourseDto>>(myCourses);
    }
}
