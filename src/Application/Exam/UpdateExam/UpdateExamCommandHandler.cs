using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auth;
using Domain.DTO.Exam;
using Domain.Exams;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Exam.UpdateExam;
internal class UpdateExamCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<UpdateExamCommand, ExamResponseDto>
{
    public async Task<Result<ExamResponseDto>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();

        var userIdGuid = Guid.Parse(userId);

        Exams? query = await unitOfWork.ExamRepository.QueryAble().Include(m => m.Instructor).FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (query == null)
        {
            return Result.Failure<ExamResponseDto>(ExamError.NotFound(request.Id));
        }

        /// fetch user role
        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userIdGuid, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure<ExamResponseDto>(UserErrors.NotFound(userIdGuid));
        }

        bool canUpdate = user.RoleId == 4 || user.RoleId == 2 || query.CreatedById == userId;

        if (!canUpdate)
        {
            return Result.Failure<ExamResponseDto>(AuthError.NotPermitted);
        }

        if (!string.IsNullOrEmpty(request.Title))
        {
            query.Title = request.Title!;
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            query.Description = request.Description!;
        }
        if (request.TotalMarks != null)
        {
            query.TotalMarks = (decimal)request.TotalMarks;
        }
        if (request.PassingMarks != null)
        {
            query.PassingMarks = (decimal)request.PassingMarks;
        }

        if (request.StartTime != null && request.EndTime != null)
        {
            query.StartTime = (DateTime)request.StartTime;
            query.EndTime = (DateTime)request.EndTime;
        }
        if (!string.IsNullOrEmpty(request.Instructions))
        {
            query.Instructions = request.Instructions;
        }
        if (!string.IsNullOrEmpty(request.Status))
        {
            query.Status = request.Status;
        }
        query.UpdatedAt = DateTime.Now;
        query.ModifiedBy = userId;

        unitOfWork.ExamRepository.Update(query);
        await unitOfWork.SaveAsync(cancellationToken);

        var updatedExamDto = (ExamResponseDto)query!;

        return Result.Success(updatedExamDto);
    }
}
