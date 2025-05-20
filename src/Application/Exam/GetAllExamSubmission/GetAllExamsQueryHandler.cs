using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Exam.GetAllCourseExam;
using Domain.DTO.Exam;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Exam.GetAllExamSubmission;
internal class GetAllExamsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAllExamsQuery, GetAllCourseExamResponse>
{
    public async Task<Result<GetAllCourseExamResponse>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllCourseExamResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllCourseExamResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<Domain.Exams.Exams> query = unitOfWork.ExamRepository.QueryAble().Include(m => m.Course).Where(m
           => (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
               (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value)
          );

        int totalItems = await query.CountAsync(cancellationToken);

        List<CreatedExamResponseDto> examScripts = await query.OrderByDescending(m => m.CreatedAt)
            .Skip((request.pageNumber - 1) * request.PageSize)
            .Take(request.PageSize).Select(m => (CreatedExamResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllCourseExamResponse
        {
            data = examScripts,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
