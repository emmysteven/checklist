using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checklist.Infrastructure.Config;

public class TodoConfig : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Chk_Todo");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.TodoName).HasMaxLength(100).IsRequired();
        
        builder.Property(s => s.TodoGroup).HasMaxLength(10).IsRequired();
        
        builder.HasIndex(s => new { s.TodoName, }).IsUnique();
    }
}