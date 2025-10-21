using Domain.Course;

namespace Application.Abstractions.Interface;

public interface ICourseTimelineMediaRepository : IRepository<CourseTimelineMedia>
{
    Task AddRangeAsync(List<CourseTimelineMedia> timeline, CancellationToken cancellationToken);
    void Update(CourseTimelineMedia timelineMedia);
    void UpdateRangeAsync(List<CourseTimelineMedia> timelineMedias, CancellationToken cancellationToken = default);
}
