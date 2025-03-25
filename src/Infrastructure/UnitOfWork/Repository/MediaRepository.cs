using Application.Abstractions.Interface;
using Domain.Media;
using Infrastructure.Database;

namespace Infrastructure.UnitOfWork.Repository;
public class MediaRepository : Repository<Domain.Media.Media>, IMediaRepository
{
    private readonly ApplicationDbContext _db;

    public MediaRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task AddRange(List<Media> uploadedMedia, CancellationToken cancellationToken)
    {
        if (uploadedMedia == null || !uploadedMedia.Any())
        {
            return;
        }

        await _db.Medias.AddRangeAsync(uploadedMedia, cancellationToken);
    }

    public void Update(Domain.Media.Media media)
    {
        _db.Medias.Update(media);
    }
}
