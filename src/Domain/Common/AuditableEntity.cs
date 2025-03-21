using Newtonsoft.Json;

namespace Domain.Common;
public abstract class AuditableEntity : IAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedById { get; set; }
    public bool IsSoftDeleted { get; set; }
    public string? ModifiedBy { get; set; }
}

public interface IAuditableEntity
{
    Guid Id { get; set; }
    [JsonProperty("created_at")]
    DateTime CreatedAt { get; set; }
    [JsonProperty("updated_at")]
    DateTime? UpdatedAt { get; set; }
    string? CreatedById { get; set; }
    string? ModifiedBy { get; set; }
    [Newtonsoft.Json.JsonIgnore]
    bool IsSoftDeleted { get; set; }
}
