using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auth;
using Domain.Media;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Media.DeleteMediaById;
internal class DeleteMediaByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext) : ICommandHandler<DeleteMediaByIdCommand>
{
    public async Task<Result> Handle(DeleteMediaByIdCommand request, CancellationToken cancellationToken)
    {
        string userId = userContext.UserId.ToString();

        if (string.IsNullOrEmpty(userId))
        {
            return Result.Failure(AuthError.NotAuthenticated);
        }

        var userIdGuid = Guid.Parse(userId);

        /// fetch user role
        User user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userIdGuid, cancellationToken: cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserErrors.NotFound(userIdGuid));
        }
        IQueryable<Domain.Media.Media> query = unitOfWork.MediaRepository.QueryAble().Include(m => m.Course).Where(m => m.Id == request.Id);
        Domain.Media.Media? media = await query.FirstOrDefaultAsync(cancellationToken);

        if (media == null)
        {
            return Result.Failure(MediaErrors.NoMediaFound);
        }

        /// check  if user has special priviledge like user.userRole = 4 || user.userRole = 2, then delete 
        /// else check if userId is == query.createdby = userid then delete 
        /// this is to ensure admin and bussiness_developer can delete any media , while user role with just instructor can only delete their own media
        bool canDelete = user.RoleId == 4 || user.RoleId == 2 || media.CreatedById == userId;

        if (!canDelete)
        {
            return Result.Failure(AuthError.NotPermitted);
        }

        media.IsSoftDeleted = true;
        media.ModifiedBy = userContext.UserId.ToString();
        media.UpdatedAt = DateTime.UtcNow;

        unitOfWork.MediaRepository.Update(media);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
