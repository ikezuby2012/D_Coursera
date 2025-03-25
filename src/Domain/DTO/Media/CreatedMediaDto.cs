namespace Domain.DTO.Media;

public class CreatedMediaDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string? CollectionName { get; set; }
    public string MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? Status { get; set; }

    public static explicit operator CreatedMediaDto(Domain.Media.Media media) => new CreatedMediaDto
    {
        Id = media.Id,
        CourseId = media.CourseId,
        CollectionName = media.CollectionName,
        MediaUrl = media.MediaUrl,
        CreatedAt = media.CreatedAt,
        CreatedById = media.CreatedById,
    };
}
