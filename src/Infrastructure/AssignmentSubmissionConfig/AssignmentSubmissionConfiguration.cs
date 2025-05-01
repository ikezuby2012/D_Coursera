using Domain.AssignmentSubmission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.AssignmentSubmissionConfig;
internal sealed class AssignmentSubmissionConfiguration : IEntityTypeConfiguration<AssignmentSubmissions>
{
    public void Configure(EntityTypeBuilder<AssignmentSubmissions> builder)
    {
        builder.ToTable("TBL_ASSIGNMENT_SUBMISSION");

        // Primary Key
        builder.HasKey(asub => asub.Id);

        builder.Property(asub => asub.AssignmentId)
                   .IsRequired();

        builder.Property(asub => asub.SubmittedById)
                   .IsRequired();

        builder.Property(asub => asub.SubmissionText)
               .IsRequired(false) // SubmissionText is optional
               .HasMaxLength(4000);

        builder.Property(asub => asub.FileUrl)
                   .IsRequired(false) // FileUrl is optional
                   .HasMaxLength(1000);

        builder.Property(asub => asub.Grade).IsRequired(false);

        builder.Property(asub => asub.Feedback)
                   .IsRequired(false) // Feedback is optional
                   .HasMaxLength(2000);
        builder.Property(asub => asub.CreatedAt)
                   .IsRequired();

        builder.Property(asub => asub.UpdatedAt)
               .IsRequired(false); // UpdatedAt is nullable

        builder.Property(asub => asub.GradedAt)
               .IsRequired(false); // GradedAt is nullable

        builder.Property(asub => asub.CreatedById)
               .IsRequired(false) // CreatedById is nullable
               .HasMaxLength(450); // Assuming CreatedById is a string with a max length of 450

        builder.Property(asub => asub.ModifiedBy)
               .IsRequired(false) // ModifiedBy is nullable
               .HasMaxLength(450); // Assuming ModifiedBy is a string with a max length of 450

        builder.Property(asub => asub.IsSoftDeleted)
               .IsRequired()
               .HasDefaultValue(false); // Default value for IsSoftDeleted is false

        builder.HasOne(asub => asub.Assignment)
                   .WithMany() // Assuming Assignment has a collection of AssignmentSubmissions
                   .HasForeignKey(asub => asub.AssignmentId)
                   .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete

        builder.HasOne(asub => asub.SubmittedBy)
               .WithMany() // Assuming User has a collection of AssignmentSubmissions
               .HasForeignKey(asub => asub.SubmittedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(asub => !asub.IsSoftDeleted);

    }
}
