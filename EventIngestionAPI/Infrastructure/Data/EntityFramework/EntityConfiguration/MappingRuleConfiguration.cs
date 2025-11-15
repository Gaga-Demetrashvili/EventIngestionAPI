using EventIngestionAPI.Entities;
using EventIngestionAPI.Enums;
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

        builder.HasData(
            new MappingRule
            {
                Id = -1,
                ExternalField = "playerId",
                InternalField = "PlayerId",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -2,
                ExternalField = "userId",
                InternalField = "PlayerId",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -3,
                ExternalField = "accountId",
                InternalField = "PlayerId",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -4,
                ExternalField = "amount",
                InternalField = "Amount",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -5,
                ExternalField = "value",
                InternalField = "Amount",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -6,
                ExternalField = "currency",
                InternalField = "Currency",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -7,
                ExternalField = "curr",
                InternalField = "Currency",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -8,
                ExternalField = "timestamp",
                InternalField = "OccurredAt",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -9,
                ExternalField = "occurredAt",
                InternalField = "OccurredAt",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            },
            new MappingRule
            {
                Id = -10,
                ExternalField = "date",
                InternalField = "OccurredAt",
                MappingRuleTypeId = (int)MappingRuleTypeEnum.Default,
                CreatedAt = new DateTime(2025, 11, 11),
                IsActive = true
            }
       );
    }
}
