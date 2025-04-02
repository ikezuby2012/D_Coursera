using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Media;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Media.GetMediaById;
internal class GetMediaByIdQueryHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetMediaByIdQuery, IEnumerable<PagedMediaResponseDto>>
{
    public async Task<Result<IEnumerable<PagedMediaResponseDto>>> Handle(GetMediaByIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Media.Media>? courses = unitOfWork.MediaRepository.QueryAble().Include(m => m.Course).Where(m => m.Id == request.Id);

        if (courses is null)
        {
            return Result.Failure<IEnumerable<PagedMediaResponseDto>>(MediaErrors.NoMediaFound);
        }

        List<PagedMediaResponseDto> result = await courses.Select(x => (PagedMediaResponseDto)x).ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<PagedMediaResponseDto>>(result);
    }
}
