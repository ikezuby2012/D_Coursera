using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExaminationConfig : IEntityTypeConfiguration<Exams>
{
    public void Configure(EntityTypeBuilder<Exams> builder)
    {
        builder.ToTable("TBL_EXAM");

        builder.HasKey(e => e.Id);

        builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(550);

        builder.Property(e => e.Description)
                .HasMaxLength(1000);

        builder.Property(e => e.Instructions)
               .HasMaxLength(2000);

        builder.Property(e => e.TotalMarks)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.PassingMarks)
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.StartTime)
                .IsRequired();

        builder.Property(e => e.EndTime)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);

        builder.Property(e => e.CreatedById)
            .HasMaxLength(450);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(450);

        builder.Property(e => e.IsSoftDeleted)
            .HasDefaultValue(false);

        builder.HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Or Cascade based on requirements

        builder.HasOne(e => e.Instructor)
            .WithMany()
            .HasForeignKey(e => e.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.CourseId);
        builder.HasIndex(e => e.InstructorId);
        builder.HasIndex(e => new { e.StartTime, e.EndTime });

        builder.ToTable(t => t.HasCheckConstraint("CK_Exams_Status",
                "[Status] IN ('Scheduled', 'InProgress', 'Completed', 'PostPoned', 'Cancelled')"));

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
