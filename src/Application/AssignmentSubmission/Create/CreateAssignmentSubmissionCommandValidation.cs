using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.AssignmentSubmission.Create;
public class CreateAssignmentSubmissionCommandValidation : AbstractValidator<CreateAssignmentSubmissionCommand>
{
    public CreateAssignmentSubmissionCommandValidation()
    {
        RuleFor(x => x.assignmentId)
                .NotEmpty().WithMessage("Assignment ID is required.");
        RuleFor(x => x.submissionText)
                .NotEmpty().WithMessage("Submission text is required.")
                .MaximumLength(5000).WithMessage("Submission text must not exceed 5000 characters.");
        RuleFor(x => x.file)
                .Must(BeAValidFile).WithMessage("The file is invalid or exceeds the allowed size (e.g., 5MB).")
                .When(x => x.file != null);
        RuleFor(x => x.feedback)
                .MaximumLength(2000).WithMessage("Feedback must not exceed 2000 characters.")
                .When(x => !string.IsNullOrEmpty(x.feedback));
    }

    private bool BeAValidFile(IFormFile? file)
    {
        if (file == null)
        {
            return true; // Allow null files (optional)
        }

        // Check file size (e.g., max 5MB)
        const int maxSizeInBytes = 5 * 1024 * 1024; // 5MB
        if (file.Length > maxSizeInBytes)
        {
            return false;
        }

        // Check file type (e.g., allow only PDF, DOCX, TXT)
        string[] allowedExtensions = new[] { ".PDF", ".DOCX", ".TXT" };
        string? fileExtension = Path.GetExtension(file.FileName)?.ToUpperInvariant();
        if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
        {
            return false;
        }

        return true;
    }

}
