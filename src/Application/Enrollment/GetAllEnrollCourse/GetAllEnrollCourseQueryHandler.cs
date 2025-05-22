using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Enrollment;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Enrollment.GetAllEnrollCourse;
internal sealed class GetAllEnrollCourseQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAllEnrollCourseQuery, GetAllEnrollCoursesResponse>
{
    public async Task<Result<GetAllEnrollCoursesResponse>> Handle(GetAllEnrollCourseQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllEnrollCoursesResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllEnrollCoursesResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<Domain.Enrollment.Enrollment> query = unitOfWork.EnrollmentRepository.QueryAble().Where(m
           => (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
               (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value) &&
               (!request.CourseId.HasValue || m.CourseId == request.CourseId.Value)
          );

        int totalItems = await query.CountAsync(cancellationToken);

        List<EnrollmentSuccessResponseDto> enrollments = await query.OrderByDescending(m => m.CreatedAt)
            .Skip((request.pageNumber - 1) * request.PageSize)
            .Take(request.PageSize).Select(m => (EnrollmentSuccessResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllEnrollCoursesResponse
        {
            data = enrollments,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
