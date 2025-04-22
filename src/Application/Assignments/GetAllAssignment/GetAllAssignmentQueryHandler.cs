using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Assignment;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

using Assignment = Domain.Assignments.Assignment;

namespace Application.Assignments.GetAllAssignment;
internal sealed class GetAllAssignmentQueryHandler(IUnitOfWork unitOfWork) : ICommandHandler<GetAllAssignmentQuery, GetAllAssignmentResponse>
{
    public async Task<Result<GetAllAssignmentResponse>> Handle(GetAllAssignmentQuery request, CancellationToken cancellationToken)
    {
        if (request.pageNumber < 1)
        {
            return Result.Failure<GetAllAssignmentResponse>(MediaErrors.InvalidPageNumber);
        }

        if (request.PageSize < 1)
        {
            return Result.Failure<GetAllAssignmentResponse>(MediaErrors.InvalidPageSize);
        }

        IQueryable<Assignment> query = unitOfWork.AssignmentRepository.QueryAble().Include(m => m.Course).Where(m
           => (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
               (!request.DateTo.HasValue || m.CreatedAt <= request.DateTo.Value) &&
               (!request.CourseId.HasValue || m.CourseId == request.CourseId.Value) &&
               (string.IsNullOrEmpty(request.CollectionName) || m.CollectionName == request.CollectionName)
          );

        int totalItems = await query.CountAsync(cancellationToken);

        List<AssigmentResponseDto> media = await query.OrderByDescending(m => m.CreatedAt)
            .Skip((request.pageNumber - 1) * request.PageSize)
            .Take(request.PageSize).Select(m => (AssigmentResponseDto)m).ToListAsync(cancellationToken);

        return Result.Success(new GetAllAssignmentResponse
        {
            data = media,
            PageNumber = request.pageNumber,
            PageSize = request.PageSize,
            TotalItems = totalItems,
        });
    }
}
