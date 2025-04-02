using Application.Abstractions.Messaging;
using Domain.DTO.Media;

namespace Application.Media.GetMediaById;
public sealed record GetMediaByIdQuery(Guid Id) : ICommand<IEnumerable<PagedMediaResponseDto>>;
