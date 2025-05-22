using Domain.Enrollment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EnrollmentConfig;
internal sealed class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
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

        builder.Property(c => c.StatusId).HasDefaultValue(1);

        // Relationship configuration
        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Course)
           .WithMany()
           .HasForeignKey(c => c.CourseId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Status).WithMany()
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.CourseId, e.UserId }).IsUnique();

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
