using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checklist.Infrastructure.Configs;

public class ItemConfig : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        // builder.HasKey(s => s.Id);
        builder.ToTable("Items");
        builder.HasKey(i => i.Id);
        builder.OwnsOne(i => i.Todo.Name);
    }
}