using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checklist.Infrastructure.Configs;

public class TodoConfig : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Checklist_Todo");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.TodoName).HasMaxLength(50).IsRequired();
        
        builder.Property(s => s.TodoGroup).HasMaxLength(10).IsRequired();
        
        builder.HasIndex(s => new {
            Name = s.TodoName, }).IsUnique();
    }
}