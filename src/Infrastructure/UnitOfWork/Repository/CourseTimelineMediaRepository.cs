using Application.Abstractions.Interface;
using Domain.Course;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;

internal sealed class CourseTimelineMediaRepository : Repository<Domain.Course.CourseTimelineMedia>, ICourseTimelineMediaRepository
{
    private readonly ApplicationDbContext _db;

    public CourseTimelineMediaRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task AddRangeAsync(List<CourseTimelineMedia> timeline, CancellationToken cancellationToken)
    {
        if (timeline == null || !timeline.Any())
        {
            return;
        }

        await _db.CourseTimelineMedias.AddRangeAsync(timeline, cancellationToken);
    }

    public void Update(CourseTimelineMedia timelineMedia)
    {
        _db.CourseTimelineMedias.Update(timelineMedia);
    }

    public void UpdateRangeAsync(List<CourseTimelineMedia> timelineMedias, CancellationToken cancellationToken = default)
    {
        _db.Set<CourseTimelineMedia>().UpdateRange(timelineMedias);
    }
}
