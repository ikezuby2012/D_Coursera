using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExamAnswerConfiguration : IEntityTypeConfiguration<ExamAnswer>
{
    public void Configure(EntityTypeBuilder<ExamAnswer> builder)
    {
        builder.ToTable("TBL_EXAM_ANSWER");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.AnswerText)
             .HasMaxLength(4000);

        builder.Property(e => e.OptionLabel).IsRequired(false)
             .HasMaxLength(4000);

        builder.Property(e => e.IsCorrect)
            .HasDefaultValue(false);

        builder.Property(e => e.IsGraded)
            .HasDefaultValue(false);

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedById)
            .HasMaxLength(450);

        builder.Property(e => e.GradedById)
           .HasMaxLength(150);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(e => e.IsSoftDeleted)
            .HasDefaultValue(false);

        builder.HasOne(e => e.Submission)
               .WithMany(a => a.Answers)
               .HasForeignKey(e => e.SubmissionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Questions)
               .WithMany()
               .HasForeignKey(e => e.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
