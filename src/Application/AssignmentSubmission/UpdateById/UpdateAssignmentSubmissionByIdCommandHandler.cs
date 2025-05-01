using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.DTO.AssignmentSubmission;
using Domain.Media;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Application.AssignmentSubmission.UpdateById;
internal sealed class UpdateAssignmentSubmissionByIdCommandHandler(IUnitOfWork unitOfWork, IUserContext userContext, Cloudinary cloudinary) : ICommandHandler<UpdateAssignmentSubmissionByIdCommand, UpdatedAssignmentSubmissionDto>
{
    public async Task<Result<UpdatedAssignmentSubmissionDto>> Handle(UpdateAssignmentSubmissionByIdCommand request, CancellationToken cancellationToken)
    {
        Domain.AssignmentSubmission.AssignmentSubmissions assignmentSubmissions = await unitOfWork.AssignmentSubmissionRepository.GetAsync(x => x.Id == request.Id, includeProperties: "Assignment", cancellationToken: cancellationToken);

        if (assignmentSubmissions is null)
        {
            return Result.Failure<UpdatedAssignmentSubmissionDto>(Domain.AssignmentSubmission.AssignmentSubmissionError.NotFound(request.Id));
        }

        if (!string.IsNullOrEmpty(request.submissionText))
        {
            assignmentSubmissions.SubmissionText = request.submissionText;
        }
        if (!string.IsNullOrEmpty(request.feedback))
        {
            assignmentSubmissions.Feedback = request.feedback;
        }

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
                    return Result.Failure<UpdatedAssignmentSubmissionDto>(MediaErrors.FailedToUploadMedia(uploadResult.Error.Message));
                }

                mediaUrl = uploadResult.SecureUrl.ToString();
                assignmentSubmissions.FileUrl = mediaUrl;
            }
            catch (Exception ex)
            {
                return Result.Failure<UpdatedAssignmentSubmissionDto>(MediaErrors.FailedToUploadMedia(ex.Message));
            }
        }

        assignmentSubmissions.ModifiedBy = userContext.UserId.ToString();
        assignmentSubmissions.UpdatedAt = DateTime.UtcNow;

        unitOfWork.AssignmentSubmissionRepository.Update(assignmentSubmissions);
        await unitOfWork.SaveAsync(cancellationToken);

        var updatedAssignmentSubmissionDto = (UpdatedAssignmentSubmissionDto)assignmentSubmissions!;

        return Result.Success(updatedAssignmentSubmissionDto);
    }
}
