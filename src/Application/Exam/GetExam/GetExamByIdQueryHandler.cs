using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.DTO.Exam;
using SharedKernel;

namespace Application.Exam.GetExam;
internal sealed class GetExamByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetExamByIdQuery, ExamResponseDto>
{
    public async Task<Result<ExamResponseDto>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
    {
        Domain.Exams.Exams? exams = await unitOfWork.ExamRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Course,Instructor", cancellationToken: cancellationToken);

        if (exams is null)
        {
            return Result.Failure<ExamResponseDto>(Domain.Exams.ExamError.NotFound(request.Id));
        }

        var examDto = (ExamResponseDto)exams;
        return Result.Success(examDto);
    }
}
