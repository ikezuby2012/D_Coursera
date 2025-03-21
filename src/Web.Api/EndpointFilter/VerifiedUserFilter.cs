
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;

namespace Web.Api.EndpointFilter;

internal sealed class VerifiedUserFilter : IEndpointFilter
{
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
            return Results.Forbid();
        }

        context.HttpContext.Items["VerifiedUser"] = user;

        return await next(context);
    }
}
