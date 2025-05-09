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
               .IsRequired(false)
               .HasMaxLength(4000);

        builder.Property(asub => asub.FileUrl)
                   .IsRequired(false)
                   .HasMaxLength(1000);

        builder.Property(asub => asub.Grade).IsRequired(false).HasPrecision(18, 2);

        builder.Property(asub => asub.Feedback)
                   .IsRequired(false) // Feedback is optional
                   .HasMaxLength(2000);
        builder.Property(asub => asub.CreatedAt)
                   .IsRequired();

        builder.Property(asub => asub.UpdatedAt)
               .IsRequired(false);

        builder.Property(asub => asub.GradedAt)
               .IsRequired(false);

        builder.Property(asub => asub.CreatedById)
               .IsRequired(false)
               .HasMaxLength(450);

        builder.Property(asub => asub.ModifiedBy)
               .IsRequired(false)
               .HasMaxLength(450);

        builder.Property(asub => asub.IsSoftDeleted)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne(asub => asub.Assignment)
                   .WithMany()
                   .HasForeignKey(asub => asub.AssignmentId)
                   .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(asub => asub.SubmittedBy)
               .WithMany()
               .HasForeignKey(asub => asub.SubmittedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(asub => !asub.IsSoftDeleted);
    }
}
