using Application.Abstractions.Messaging;
using Domain.DTO.Courses;

namespace Application.Courses.GetAllCourses;
public sealed record GetAllCourseCommand() : ICommand<IEnumerable<GetAllCoursesDto>>;
