using Domain.Assignments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.AssignmentConfig;
internal sealed class AssigmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("TBL_ASSIGNMENT");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Title)
                   .IsRequired()
                   .HasMaxLength(255);

        builder.Property(a => a.Description)
                   .IsRequired(false) // Description is optional
                   .HasMaxLength(1000);

        builder.Property(a => a.CourseId)
                   .IsRequired();

        builder.Property(a => a.CollectionName)
                   .IsRequired(false) // CollectionName is nullable
                   .HasMaxLength(255);

        builder.Property(a => a.MaxScore)
                   .IsRequired()
                   .HasPrecision(18, 2);

        builder.Property(a => a.AssignmentTypeId)
                   .IsRequired(false);

        builder.Property(a => a.DueDate)
                   .IsRequired();

        builder.Property(a => a.CreatedAt)
               .IsRequired();

        builder.Property(a => a.UpdatedAt)
               .IsRequired(false);

        builder.Property(a => a.CreatedById)
                   .IsRequired(false) // CreatedById is nullable
                   .HasMaxLength(450);

        builder.Property(a => a.ModifiedBy)
              .IsRequired(false) // ModifiedBy is nullable
              .HasMaxLength(450); // Assuming ModifiedBy is a string with a max length of 450

        builder.Property(a => a.IsSoftDeleted)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasOne(a => a.Course)
                   .WithMany() // Assuming Course has a collection of Assignments
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.AssignmentTypes)
                   .WithMany() // Assuming AssignmentTypes has a collection of Assignments
                   .HasForeignKey(a => a.AssignmentTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(a => !a.IsSoftDeleted);
    }
}
