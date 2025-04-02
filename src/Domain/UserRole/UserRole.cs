using Domain.Common;

namespace Domain.UserRole;
public sealed class UserRoles : Enumeration<UserRoles>
{
    public static readonly UserRoles User = new(1, "User");
    public static readonly UserRoles BusinessDeveloper = new(2, "Business_Developer");
    public static readonly UserRoles Instructor = new(3, "Instructor");
    public static readonly UserRoles Admin = new(4, "Admin");
    private UserRoles(int Id, string name) : base(Id, name) { }
}
