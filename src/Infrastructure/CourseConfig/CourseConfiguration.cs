using Microsoft.EntityFrameworkCore;
using Domain.Course;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.CourseConfig;
internal sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("TBL_COURSES");

        builder.HasKey(u => u.Id);

        builder.Property(c => c.Title).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Duration)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Prerequisites).HasMaxLength(4000).IsRequired(false);

        builder.Property(c => c.Capacity).HasDefaultValue(500).IsRequired(false);

        builder.Property(c => c.IsPaid).HasDefaultValue(false);

        builder.Property(c => c.Availability)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(c => c.InstructorId)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired(false);

        builder.Property(c => c.CreatedById)
            .IsRequired(false)
            .HasMaxLength(450);

        builder.Property(c => c.ModifiedBy)
            .IsRequired(false)
            .HasMaxLength(450);

        builder.Property(c => c.IsSoftDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Relationship configuration
        builder.HasOne(c => c.Instructor)
            .WithMany()
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.MediaUrls)
               .WithOne(m => m.Course)
               .HasForeignKey(m => m.CourseId)
               .OnDelete(DeleteBehavior.Restrict);

        // Query filter for soft delete
        builder.HasQueryFilter(c => !c.IsSoftDeleted);
    }
}
