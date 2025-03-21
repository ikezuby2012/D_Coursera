using Application.Abstractions.Messaging;

namespace Application.Courses.DeleteCourseById;
public sealed record DeleteCommand(Guid Id) : ICommand<Guid>;

