using Checklist.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Checklist.Infrastructure.Persistence.Config;

public class SummaryConfig : IEntityTypeConfiguration<Summary>
{
    public void Configure(EntityTypeBuilder<Summary> builder)
    {
        builder.ToTable("Chk_Summary");
        
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Makers).HasMaxLength(50).IsRequired();
        
        builder.Property(s => s.Checkers).HasMaxLength(50).IsRequired();
        
        builder.Property(s => s.Dbas).HasMaxLength(50).IsRequired();
        
        builder.Property(s => s.EodDate).IsRequired();
        
        builder.Property(s => s.TxnCount).IsRequired();
    }
}