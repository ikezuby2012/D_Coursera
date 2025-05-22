using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Assignments.GetAllAssignment;
using Application.Enrollment.GetAllEnrollCourse;
using Domain.Assignments;
using Domain.DTO.Assignment;
using Domain.DTO.Enrollment;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Enrollment.GetMineEnrollCourse;
internal class GetMineEnrollCourseQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext) : IQueryHandler<GetMineEnrollCoursesQuery, GetAllEnrollCoursesResponse>
{
    public async Task<Result<GetAllEnrollCoursesResponse>> Handle(GetMineEnrollCoursesQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllEnrollCoursesResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllEnrollCoursesResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<Domain.Enrollment.Enrollment> query = unitOfWork.EnrollmentRepository.QueryAble().Include(m => m.Course).Where(m
          => m.UserId == userContext.UserId && (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
              (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value)
         );

        int totalItems = await query.CountAsync(cancellationToken);

        List<EnrollmentSuccessResponseDto> enrolledCourses = await query.OrderByDescending(m => m.CreatedAt)
           .Skip((request.pageNumber - 1) * request.PageSize)
           .Take(request.PageSize).Select(m => (EnrollmentSuccessResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllEnrollCoursesResponse
        {
            data = enrolledCourses,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
