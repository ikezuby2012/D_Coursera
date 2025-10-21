using Application.Abstractions.Interface;

namespace Application.Abstractions.Data;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICourseRepository CourseRepository { get; }
    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
    IMediaRepository MediaRepository { get; }
    IAssignmentRepository AssignmentRepository { get; }
    IAssignmentSubmissionRepository AssignmentSubmissionRepository { get; }
    IExamRepository ExamRepository { get; }
    IExamQuestionsRepository ExamQuestionsRepository { get; }
    IExamQuestionOptionsRepository ExamQuestionOptionsRepository { get; }
    IExamSubmissionRepository ExamSubmissionRepository { get; }
    IExamAnswerRepository ExamAnswerRepository { get; }
    IEnrollmentRepository EnrollmentRepository { get; }
    ICourseTimelineMediaRepository CourseTimelineMediaRepository { get; }
}
