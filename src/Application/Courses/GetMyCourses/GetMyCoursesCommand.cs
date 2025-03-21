using Application.Abstractions.Messaging;
using Domain.DTO.Courses;

namespace Application.Courses.GetMyCourses;
public sealed record GetMyCoursesCommand : ICommand<IEnumerable<CreatedCourseDto>>;
