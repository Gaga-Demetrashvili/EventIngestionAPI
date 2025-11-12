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
            .IsRequired();

        builder.Property(mr => mr.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasData(
            new MappingRule
            {
                Id = 1,
                ExternalField = "usr",
                InternalField = "PlayerId",
                IsActive = true,
                CreatedAt = DateTime.Now
            },
            new MappingRule
            {
                Id = 2,
                ExternalField = "amt",
                InternalField = "Amount",
                IsActive = true,
                CreatedAt = DateTime.Now
            }
        );
    }
}
