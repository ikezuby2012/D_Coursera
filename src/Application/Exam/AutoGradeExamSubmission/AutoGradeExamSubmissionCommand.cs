using Application.Abstractions.Messaging;
using Domain.DTO.Exam;

namespace Application.Exam.AutoGradeExamSubmission;
public sealed record AutoGradeExamSubmissionCommand(
        Guid ExamId,
        DateTime StartTime, /// when candidate start the exam
        DateTime EndTime, /// when candidate finsished the exam
        List<ExamSubmissionRequestDto> AnswersScripts
    ) : ICommand<AutoGradeExamSubmissionResponse>;
