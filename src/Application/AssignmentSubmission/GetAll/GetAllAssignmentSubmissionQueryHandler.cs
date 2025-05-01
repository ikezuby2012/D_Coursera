using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.AssignmentSubmission;
using Domain.DTO.AssignmentSubmission;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.AssignmentSubmission.GetAll;
internal sealed class GetAllAssignmentSubmissionQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetAllAssignmentSubmissionQuery, GetAllAssignmentSubmissionResponse>
{
    public async Task<Result<GetAllAssignmentSubmissionResponse>> Handle(GetAllAssignmentSubmissionQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllAssignmentSubmissionResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllAssignmentSubmissionResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<AssignmentSubmissions> query = unitOfWork.AssignmentSubmissionRepository.QueryAble().Include(m => m.Assignment).Where(m
           => (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
               (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value) &&
               (!request.assignmentId.HasValue || m.AssignmentId == request.assignmentId.Value)
          );

        int totalItems = await query.CountAsync(cancellationToken);

        List<AssignmentSubmissionResponseDto> data = await query.OrderByDescending(m => m.CreatedAt)
           .Skip((request.pageNumber - 1) * request.PageSize)
           .Take(request.PageSize).Select(m => (AssignmentSubmissionResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllAssignmentSubmissionResponse
        {
            data = data,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
