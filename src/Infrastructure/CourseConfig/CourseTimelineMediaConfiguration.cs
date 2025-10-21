using Domain.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.CourseConfig;
internal sealed class CourseTimelineMediaConfiguration : IEntityTypeConfiguration<CourseTimelineMedia>
{
    public void Configure(EntityTypeBuilder<CourseTimelineMedia> builder)
    {
        builder.HasKey(x => x.Id);

        // Table name (optional, but explicit)
        builder.ToTable("TBL_CourseTimelineMedia");

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.CourseId)
               .IsRequired();

        builder.Property(x => x.MediaUrl)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.FileFormat)
               .HasMaxLength(50);

        builder.Property(x => x.FilePath)
              .HasMaxLength(500);

        builder.Property(x => x.UploadedById)
               .IsRequired();

        builder.Property(x => x.CreatedById)
               .HasMaxLength(450);

        builder.Property(x => x.ModifiedBy)
               .HasMaxLength(450);

        builder.Property(x => x.IsSoftDeleted)
               .HasDefaultValue(false);

        builder.HasOne(x => x.Course)
              .WithMany(x => x.TimelineMedias)
              .HasForeignKey(x => x.CourseId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UploadedBy)
               .WithMany()
               .HasForeignKey(x => x.UploadedById)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(e => !e.IsSoftDeleted);
    }
}
