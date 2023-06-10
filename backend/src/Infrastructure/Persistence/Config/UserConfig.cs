using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checklist.Infrastructure.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Chk_User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).HasMaxLength(20).IsRequired();
        builder.Property(u => u.Role).HasMaxLength(8).IsRequired();
    }
}