using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Course;
using Domain.DTO.Courses;
using Domain.DTO.Media;
using Domain.Media;
using SharedKernel;

namespace Application.Media.UpdateMediaById;
internal sealed class UpdateMediaByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, Cloudinary cloudinary) : ICommandHandler<UpdateMediaByIdCommand, UpdatedMediaDto>
{
    public async Task<Result<UpdatedMediaDto>> Handle(UpdateMediaByIdCommand request, CancellationToken cancellationToken)
    {
        Domain.Media.Media? media = await unitOfWork.MediaRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Course", tracked: true, cancellationToken);

        if (media is null)
        {
            return Result.Failure<UpdatedMediaDto>(MediaErrors.NoMediaFound);
        }
        string collectionName = string.IsNullOrEmpty(request.collectionName) ? media.CollectionName : request.collectionName;

        if (request.Files != null)
        {
            foreach (Microsoft.AspNetCore.Http.IFormFile file in request.Files)
            {
                if (file is null || file.Length == 0)
                {
                    continue;
                }

                try
                {
                    await using Stream stream = file.OpenReadStream();


                    var uploadedParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        PublicId = $"{collectionName}/{Guid.NewGuid()}",
                        Folder = $"courses/{request.courseId}",
                    };

                    ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadedParams);

                    if (uploadResult.Error != null)
                    {
                        return Result.Failure<UpdatedMediaDto>(MediaErrors.FailedToUploadMedia(uploadResult.Error.Message));
                    }

                    media.MediaUrl = uploadResult.SecureUrl.ToString();
                }
                catch (Exception ex)
                {
                    return Result.Failure<UpdatedMediaDto>(MediaErrors.FailedToUploadMedia(ex.Message));
                }
            }
        }

        if (!string.IsNullOrEmpty(request.collectionName))
        {
            media.CollectionName = request.collectionName;
        }

        media.UpdatedAt = DateTime.Now;
        media.ModifiedBy = userContext.UserId.ToString();

        unitOfWork.MediaRepository.Update(media);
        await unitOfWork.SaveAsync(cancellationToken);

        var updatedMediaDto = (UpdatedMediaDto)media!;

        return Result.Success(updatedMediaDto);
    }
}
