using Application.Abstractions.Messaging;
using Domain.DTO.Media;

namespace Application.Media.GetCourseMedia;
public sealed record GetAllCourseMediaQuery(Guid courseId) : ICommand<IEnumerable<PagedMediaResponseDto>>;
