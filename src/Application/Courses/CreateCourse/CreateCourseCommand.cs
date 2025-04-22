using Application.Abstractions.Messaging;
using Domain.DTO.Courses;

namespace Application.Courses.CreateCourse;
public sealed record CreateCourseCommand(string title, string Description, string Duration, bool Availability) : ICommand<CreatedCourseDto>;
