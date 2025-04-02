using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Media;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Media.GetAllMedia;
internal sealed class GetAllMediaQueryHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetMediaQuery, GetAllMediaResponse>
{
    public async Task<Result<GetAllMediaResponse>> Handle(GetMediaQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllMediaResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllMediaResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<Domain.Media.Media> query = unitOfWork.MediaRepository.QueryAble().Include(m => m.Course).Where(m
            => (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
                (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value) &&
                (!request.CourseId.HasValue || m.CourseId == request.CourseId.Value) &&
                (string.IsNullOrEmpty(request.CollectionName) || m.CollectionName == request.CollectionName)
           );

        int totalItems = await query.CountAsync(cancellationToken);

        List<PagedMediaResponseDto> media = await query.OrderByDescending(m => m.CreatedAt)
            .Skip((request.pageNumber - 1) * request.PageSize)
            .Take(request.PageSize).Select(m => (PagedMediaResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllMediaResponse
        {
            data = media,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
