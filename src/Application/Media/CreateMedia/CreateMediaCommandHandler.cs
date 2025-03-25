using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Course;
using Domain.DTO.Media;
using Domain.Media;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Media.CreateMedia;
internal sealed class CreateMediaCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, Cloudinary cloudinary, IApplicationDbContext context) : ICommandHandler<CreateMediaCommand, IEnumerable<CreatedMediaDto>>
{
    public async Task<Result<IEnumerable<CreatedMediaDto>>> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
    {
        Course? course = await unitOfWork.CourseRepository.GetAsync(x => x.Id == request.courseId, cancellationToken: cancellationToken);

        if (course is null)
        {
            return Result.Failure<IEnumerable<CreatedMediaDto>>(CourseErrors.NoCoursesFound);
        }

        if (request.files == null || !request.files.Any())
        {
            return Result.Failure<IEnumerable<CreatedMediaDto>>(
                MediaErrors.FailedToUploadMedia("The files collection is empty.")
            );
        }

        if (course.InstructorId.ToString() != userContext.UserId.ToString())
        {
            return Result.Failure<IEnumerable<CreatedMediaDto>>(MediaErrors.NotAuthorizedToCreate);
        }

        var uploadedMedia = new List<Domain.Media.Media>();
        var createdMediaDtos = new List<CreatedMediaDto>();

        foreach (Microsoft.AspNetCore.Http.IFormFile file in request.files)
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
                    PublicId = $"{request.collectionName}/{Guid.NewGuid()}",
                    Folder = $"courses/{request.courseId}",
                };

                ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadedParams);

                if (uploadResult.Error != null)
                {
                    return Result.Failure<IEnumerable<CreatedMediaDto>>(MediaErrors.FailedToUploadMedia(uploadResult.Error.Message));
                }

                var media = new Domain.Media.Media
                {
                    CollectionName = request.collectionName,
                    MediaUrl = uploadResult.SecureUrl.ToString(),
                    CourseId = request.courseId,
                    CreatedById = userContext.UserId.ToString(),
                    CreatedAt = DateTime.UtcNow,
                };

                uploadedMedia.Add(media);
                createdMediaDtos.Add(new CreatedMediaDto
                {
                    Id = media.Id,
                    Status = uploadResult.Status,
                    MediaUrl = media.MediaUrl,
                    CollectionName = request.collectionName,
                    CourseId = request.courseId,
                    CreatedById = userContext.UserId.ToString(),
                    CreatedAt = DateTime.UtcNow,
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CreatedMediaDto>>(MediaErrors.FailedToUploadMedia(ex.Message));
            }
        }
        using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.DatabaseFacade.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.MediaRepository.AddRange(uploadedMedia, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            //logger.LogError("error occured: {0} -- {1}", nameof(CreateMediaCommandHandler), ex.Message);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<IEnumerable<CreatedMediaDto>>(MediaErrors.FailedToUploadMedia(ex.Message));
        }
        return Result.Success<IEnumerable<CreatedMediaDto>>(createdMediaDtos);
    }
}
