using Application.Abstractions.Messaging;
using Domain.DTO.Courses;

namespace Application.Courses.UpdateCourseById;

public sealed record UpdateCourseByIdCommand(Guid Id, string title, string Description, string Duration, bool Availability, double price) : ICommand<CreatedCourseDto>;

