using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExamQuestionOptionConfiguration : IEntityTypeConfiguration<ExamQuestionOption>
{
    public void Configure(EntityTypeBuilder<ExamQuestionOption> builder)
    {
        builder.ToTable("TBL_EXAM_QUESTION_OPTION");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.OptionText)
              .HasMaxLength(4000);

        builder.Property(e => e.OptionLabel).HasMaxLength(3000);

        builder.Property(e => e.IsCorrect).HasDefaultValue(false);

        builder.Property(e => e.CreatedAt).IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedById)
            .HasMaxLength(450);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(e => e.IsSoftDeleted)
            .HasDefaultValue(false);

        builder.HasOne(e => e.Question)
               .WithMany(q => q.Options)
               .HasForeignKey(e => e.QuestionId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
