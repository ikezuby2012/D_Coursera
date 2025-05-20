using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExamSubmissionConfiguration : IEntityTypeConfiguration<ExamsSubmission>
{
    public void Configure(EntityTypeBuilder<ExamsSubmission> builder)
    {
        builder.ToTable("TBL_EXAM_SUBMISSION");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TotalScore)
           .HasColumnType("decimal(18,2)").IsRequired(false);

        builder.Property(e => e.IsGraded).HasDefaultValue(false);

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.GradedAt);

        builder.Property(e => e.CreatedById)
            .HasMaxLength(450);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(e => e.IsSoftDeleted)
            .HasDefaultValue(false);

        builder.Property(e => e.StartTime).IsRequired();

        builder.Property(e => e.EndTime).IsRequired();

        builder.HasOne(e => e.Student)
               .WithMany()
               .HasForeignKey(e => e.StudentId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.GradedBy)
              .WithMany()
              .HasForeignKey(e => e.GradedById)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Exams)
               .WithMany()
               .HasForeignKey(e => e.ExamId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Answers).WithOne(e => e.Submission).HasForeignKey(e => e.SubmissionId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
