using Domain.Exams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ExamConfig;
internal sealed class ExamTypeConfiguration : IEntityTypeConfiguration<ExamType>
{
    public void Configure(EntityTypeBuilder<ExamType> builder)
    {
        builder.ToTable("TBL_EXAM_TYPES");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(500);

        builder.HasData(ExamType.GetValues());
    }
}
