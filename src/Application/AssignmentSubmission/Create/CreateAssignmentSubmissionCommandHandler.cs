using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.DTO.AssignmentSubmission;
using Domain.Media;
using Microsoft.AspNetCore.Http;
using SharedKernel;

using AssignmentSubmissions = Domain.AssignmentSubmission.AssignmentSubmissions;

namespace Application.AssignmentSubmission.Create;
internal sealed class CreateAssignmentSubmissionCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, Cloudinary cloudinary) : ICommandHandler<CreateAssignmentSubmissionCommand, CreatedAssignmentSubmissionDto>
{
    public async Task<Result<CreatedAssignmentSubmissionDto>> Handle(CreateAssignmentSubmissionCommand request, CancellationToken cancellationToken)
    {
        string mediaUrl = "";

        if (request.file != null)
        {
            try
            {
                IFormFile file = request.file;
                await using Stream stream = file.OpenReadStream();

                var uploadedParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = $"{request.assignmentId}/{Guid.NewGuid()}",
                    Folder = $"assignment/{request.assignmentId}",
                };

                ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadedParams);

                if (uploadResult.Error != null)
                {
                    return Result.Failure<CreatedAssignmentSubmissionDto>(MediaErrors.FailedToUploadMedia(uploadResult.Error.Message));
                }

                mediaUrl = uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                return Result.Failure<CreatedAssignmentSubmissionDto>(MediaErrors.FailedToUploadMedia(ex.Message));
            }
        }

        var submitAssignment = new AssignmentSubmissions
        {
            AssignmentId = request.assignmentId,
            SubmittedById = Guid.Parse(userContext.UserId.ToString()),
            SubmissionText = request.submissionText,
            FileUrl = mediaUrl,
            CreatedAt = DateTime.Now,
            Feedback = request.feedback,
            CreatedById = !string.IsNullOrEmpty(userContext.UserId.ToString()) ? userContext.UserId.ToString() : null,
            IsSoftDeleted = false
        };

        await unitOfWork.AssignmentSubmissionRepository.AddAsync(submitAssignment);
        await unitOfWork.SaveAsync(cancellationToken);

        AssignmentSubmissions? submittedAssignment = await unitOfWork.AssignmentSubmissionRepository.GetAsync(x => x.Id == submitAssignment.Id, includeProperties: "Assignment", cancellationToken: cancellationToken);

        var submittedAssignmentDto = (CreatedAssignmentSubmissionDto)submittedAssignment!;

        return Result.Success(submittedAssignmentDto);
    }
}
