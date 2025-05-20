using Application.Abstractions.Authentication;
using Application.Abstractions.Data;

namespace Web.Api.EndpointFilter;

internal sealed class RolePermissionFilter : IEndpointFilter
{
    private readonly string _requiredRole;

    public RolePermissionFilter(string requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        IUserContext userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContext>();

        Guid userId = userContext.UserId;

        IApplicationDbContext dbContext = context.HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();

        Domain.Users.User? user = await dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            return Results.NotFound($"user with id {userId} is not found");
        }

        if (!user.isVerifed || !user.IsActive)
        {
            return Results.Json(new { success = false, message = "User not yet verified" }, statusCode: 401);
        }

        string? userRoleName = Domain.UserRole.UserRoles.FromValue((int)user.RoleId!)?.Name;

        if (!string.Equals(userRoleName, _requiredRole, StringComparison.OrdinalIgnoreCase))
        {
            return Results.Json(new { success = false, message = "Role is not permitted" }, statusCode: 403);
        }

        context.HttpContext.Items["VerifiedUser"] = user;

        return await next(context);
    }
}
