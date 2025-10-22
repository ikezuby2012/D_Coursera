using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Course;
using Domain.DTO.Courses;
using Domain.Media;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Application.Courses.CreateCourse;
internal sealed class CreatedCourseCommandHandler(IUnitOfWork unitOfWork,
    IUserContext userContext,
    IDateTimeProvider dateTimeProvider,
    Cloudinary cloudinary) : ICommandHandler<CreateCourseCommand, CreatedCourseDto>
{

    public async Task<Result<CreatedCourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {

        User? user = await unitOfWork.UserRepository.GetAsync(u => u.Id == userContext.UserId, cancellationToken: cancellationToken);

        if (user is null)
        {
            return Result.Failure<CreatedCourseDto>(UserErrors.NotFound(userContext.UserId));
        }

        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = request.title.Trim(),
            Description = request.Description,
            Duration = request.Duration,
            Availability = request.Availability,
            InstructorId = userContext.UserId,
            CreatedAt = dateTimeProvider.UtcNow,
            CourseLevel = request.CourseLevel,
            Category = request.Category,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Language = request.Language,
            TimeZone = request.TimeZones,
            CreatedById = !string.IsNullOrEmpty(userContext.UserId.ToString()) ? userContext.UserId.ToString() : null,
            IsSoftDeleted = false
        };

        await unitOfWork.CourseRepository.AddAsync(course);

        var timelineMedia = new List<CourseTimelineMedia>();

        if (request.files != null && request.files.Any())
        {
            Result<bool> result = await HandleMediaUpload(request.files, timelineMedia, course.Id, cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<CreatedCourseDto>(MediaErrors.FailedToUploadMedia("something went wrong"));
            }

            await unitOfWork.CourseTimelineMediaRepository.AddRangeAsync(timelineMedia, cancellationToken);
        }

        await unitOfWork.SaveAsync(cancellationToken);

        course.Instructor = user;

        var createdCourseDto = (CreatedCourseDto)course;

        return Result.Success(createdCourseDto);
    }

    private async Task<Result<bool>> HandleMediaUpload(
        IEnumerable<IFormFile> files,
        List<CourseTimelineMedia> timelineMediaListToPopulate,
        Guid courseId,
        CancellationToken cancellationToken)
    {
        foreach (IFormFile file in files)
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
                    PublicId = $"timeline-media/{Guid.NewGuid()}",
                    Folder = $"courses/{courseId}",
                };

                ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadedParams, cancellationToken);

                if (uploadResult.Error != null)
                {
                    return Result.Failure<bool>(MediaErrors.FailedToUploadMedia(uploadResult.Error.Message));
                }

                timelineMediaListToPopulate.Add(new CourseTimelineMedia
                {
                    MediaUrl = uploadResult.SecureUrl.ToString(),
                    CourseId = courseId,
                    CreatedById = userContext.UserId.ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UploadedById = userContext.UserId
                });

            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(MediaErrors.FailedToUploadMedia(ex.Message));
            }
        }

        return Result.Success(true);
    }
}
