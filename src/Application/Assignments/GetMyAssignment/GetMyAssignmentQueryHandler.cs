using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Assignments.GetAllAssignment;
using Domain.Assignments;
using Domain.DTO.Assignment;
using Domain.Media;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Assignments.GetMyAssignment;
internal sealed class GetMyAssignmentQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<GetMyAssignmentQuery, GetAllAssignmentResponse>
{
    public async Task<Result<GetAllAssignmentResponse>> Handle(GetMyAssignmentQuery request, CancellationToken cancellationToken)
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
          => m.CreatedById == userContext.UserId.ToString() && (!request.DateFrom.HasValue || m.CreatedAt >= request.DateFrom.Value) &&
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
