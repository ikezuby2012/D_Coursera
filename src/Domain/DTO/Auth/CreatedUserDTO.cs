using Domain.Users;

namespace Domain.DTO.Auth;
public class CreatedUserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OTP { get; set; }
    public int? RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool isVerifed { get; set; }

    public static explicit operator CreatedUserDto(User user) => new CreatedUserDto
    {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        OTP = user.OTP,
        RoleId = user.RoleId,
        CreatedAt = user.CreatedAt,
        UpdatedAt = user.UpdatedAt,
        IsActive = user.IsActive,
        isVerifed = user.isVerifed
    };
}
