using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Media;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Media.GetCourseMedia;
internal sealed class GetAllCourseMediaQueryHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetAllCourseMediaQuery, IEnumerable<PagedMediaResponseDto>>
{
    public async Task<Result<IEnumerable<PagedMediaResponseDto>>> Handle(GetAllCourseMediaQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Media.Media> courseMedia = unitOfWork.MediaRepository.QueryAble().Include(m => m.Course).Where(m => m.CourseId == request.courseId);

        if (courseMedia == null)
        {
            return Result.Failure<IEnumerable<PagedMediaResponseDto>>(MediaErrors.NoMediaFound);
        }

        List<PagedMediaResponseDto> results = await courseMedia.Select(x => (PagedMediaResponseDto)x).ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<PagedMediaResponseDto>>(results);
    }
}
