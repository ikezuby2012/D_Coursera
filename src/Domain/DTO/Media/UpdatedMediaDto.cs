using Domain.DTO.Courses;

namespace Domain.DTO.Media;
public class UpdatedMediaDto
{
    public Guid Id { get; set; }
    public GetAllCoursesDto? Course { get; set; }
    public string? CollectionName { get; set; }
    public string MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }

    public static explicit operator UpdatedMediaDto(Domain.Media.Media media) => new UpdatedMediaDto
    {
        Id = media.Id,
        CollectionName = media.CollectionName,
        MediaUrl = media.MediaUrl,
        CreatedAt = media.CreatedAt,
        UpdatedAt = media.UpdatedAt,
        CreatedById = media.CreatedById,
        ModifiedBy = media.ModifiedBy,
        Course = media.Course != null
            ? (GetAllCoursesDto)media.Course
            : null
    };
}
