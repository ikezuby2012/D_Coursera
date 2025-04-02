using Application.Abstractions.Messaging;

namespace Application.Media.GetAllMedia;

public sealed record GetMediaQuery(
    int PageSize = 1000,
    int pageNumber = 0,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    Guid? CourseId = null,
    string? CollectionName = null) : ICommand<GetAllMediaResponse>;
