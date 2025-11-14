using EventIngestionAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework.EntityConfiguration;

public class MappingRuleConfiguration : IEntityTypeConfiguration<MappingRule>
{
    public void Configure(EntityTypeBuilder<MappingRule> builder)
    {
        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.ExternalField)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mr => mr.InternalField)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mr => mr.IsActive)
            .HasDefaultValue(true);

        builder.Property(mr => mr.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(mr => mr.MappingRuleType)
            .WithMany();
    }
}
