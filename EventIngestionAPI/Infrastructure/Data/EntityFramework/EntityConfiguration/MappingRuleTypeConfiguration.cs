using EventIngestionAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework.EntityConfiguration;

public class MappingRuleTypeConfiguration : IEntityTypeConfiguration<MappingRuleType>
{
    public void Configure(EntityTypeBuilder<MappingRuleType> builder)
    {
        builder.HasKey(mrt => mrt.Id);

        builder.Property(mrt => mrt.Type)
                  .IsRequired()
                  .HasMaxLength(100);

        builder.HasData(
            new MappingRuleType
            {
                Id = 1,
                Type = "default"
            },
            new MappingRuleType
            {
                Id = 2,
                Type = "dynamic"
            }
        );
    }
}
