using Application.Abstractions.Messaging;
using Domain.DTO.Courses;

namespace Application.Courses.GetById;

public sealed record GetCourseByIdQuery(Guid Id) : IQuery<CreatedCourseDto>;
