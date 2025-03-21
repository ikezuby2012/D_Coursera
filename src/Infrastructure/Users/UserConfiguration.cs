using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("TBL_USERS");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(256)
               .HasColumnName("EMAIL");

        builder.Property(u => u.FirstName)
               .HasMaxLength(100)
               .HasColumnName("FIRST_NAME");

        builder.Property(u => u.LastName)
               .HasMaxLength(100)
               .HasColumnName("LAST_NAME");

        builder.Property(u => u.OTP)
               .HasMaxLength(6)
               .HasColumnName("OTP");

        builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(512)
               .HasColumnName("PASSWORD_HASH");

        builder.Property(u => u.CreatedAt)
               .HasColumnName("CREATED_AT")
               .IsRequired();

        builder.Property(u => u.UpdatedAt)
               .HasColumnName("UPDATED_AT");

        builder.Property(u => u.LastLogin)
               .HasColumnName("LAST_LOGIN");

        builder.Property(u => u.CreatedById)
               .HasMaxLength(128)
               .HasColumnName("CREATED_BY_ID");

        builder.Property(u => u.ModifiedBy)
               .HasMaxLength(128)
               .HasColumnName("MODIFIED_BY");

        builder.Property(u => u.IsSoftDeleted)
               .HasColumnName("IS_SOFT_DELETED")
               .HasDefaultValue(false);

        builder.Property(u => u.RoleId)
                .HasColumnName("ROLE_ID");

        builder.HasOne(u => u.UserRole)
            .WithMany()
            .HasForeignKey(u => u.RoleId);

        builder.Property(u => u.isVerifed)
              .HasColumnName("IS_VERIFIED")
              .HasDefaultValue(false);

        builder.Property(u => u.IsActive)
              .HasColumnName("IS_ACTIVE")
              .HasDefaultValue(true);
    }
}
