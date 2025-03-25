namespace Application.Abstractions.Interface;
public interface IMediaRepository : IRepository<Domain.Media.Media>
{
    Task AddRange(List<Domain.Media.Media> uploadedMedia, CancellationToken cancellationToken);
    void Update(Domain.Media.Media media);
}
