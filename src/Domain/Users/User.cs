using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.UserRole;
using SharedKernel;

namespace Domain.Users;

[Table("TBL_USERS")]
public sealed class User : Entity, IAuditableEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OTP { get; set; }
    public string PasswordHash { get; set; }
    public int? RoleId { get; set; }
    public UserRoles? UserRole { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public string? CreatedById { get; set; }
    public bool IsSoftDeleted { get; set; }
    public bool IsActive { get; set; } = true;
    public bool isVerifed { get; set; }
    public string? ModifiedBy { get; set; }
}
