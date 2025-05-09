using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestions>
{
    public void Configure(EntityTypeBuilder<ExamQuestions> builder)
    {
        builder.ToTable("TBL_EXAM_QUESTIONS");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.QuestionText)
              .HasMaxLength(4000);

        builder.Property(e => e.CreatedAt)
           .IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedById)
            .HasMaxLength(450);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(e => e.IsSoftDeleted)
            .HasDefaultValue(false);

        builder.HasOne(e => e.Exam)
                .WithMany()
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Type).WithMany()
            .HasForeignKey(e => e.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Options).WithOne(e => e.Question).HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
