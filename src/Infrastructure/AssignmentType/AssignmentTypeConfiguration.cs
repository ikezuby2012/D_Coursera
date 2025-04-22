using Domain.AssignmentType;
using Domain.UserRole;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.AssignmentType;
internal class AssignmentTypeConfiguration : IEntityTypeConfiguration<AssignmentTypes>
{
    public void Configure(EntityTypeBuilder<AssignmentTypes> builder)
    {
        builder.ToTable("TBL_ASSIGNMENT_TYPES");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder.HasData(AssignmentTypes.GetValues());
    }
}
