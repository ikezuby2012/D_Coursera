using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using SharedKernel;

namespace Domain.Media;

[Table("TBL_MEDIA")]
public sealed class Media : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Domain.Course.Course Course { get; set; }
    public string? CollectionName { get; set; }
    public string MediaUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsSoftDeleted { get; set; }
}
