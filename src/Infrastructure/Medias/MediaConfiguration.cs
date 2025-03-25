using Domain.Media;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Medias;
internal sealed class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("TBL_MEDIA");

        // Primary key
        builder.HasKey(m => m.Id);

        // Properties configuration
        builder.Property(m => m.Id)
            .IsRequired();

        builder.Property(m => m.CourseId)
            .IsRequired();

        builder.Property(m => m.CollectionName)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(m => m.MediaUrl)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.CreatedAt)
            .IsRequired();

        builder.Property(m => m.UpdatedAt)
            .IsRequired(false);

        builder.Property(m => m.CreatedById)
            .HasMaxLength(450)
            .IsRequired(false);

        builder.Property(m => m.ModifiedBy)
            .HasMaxLength(450)
            .IsRequired(false);

        builder.Property(m => m.IsSoftDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(m => m.Course)
                .WithMany(c => c.MediaUrls)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

        // Query filter for soft delete
        builder.HasQueryFilter(m => !m.IsSoftDeleted);
    }
}
