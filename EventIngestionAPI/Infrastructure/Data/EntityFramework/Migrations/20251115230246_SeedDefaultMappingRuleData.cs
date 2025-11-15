using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultMappingRuleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MappingRules",
                columns: new[] { "Id", "CreatedAt", "ExternalField", "InternalField", "IsActive", "MappingRuleTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { -10, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "date", "OccurredAt", true, 1, null },
                    { -9, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "occurredAt", "OccurredAt", true, 1, null },
                    { -8, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "timestamp", "OccurredAt", true, 1, null },
                    { -7, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "curr", "Currency", true, 1, null },
                    { -6, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "currency", "Currency", true, 1, null },
                    { -5, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "value", "Amount", true, 1, null },
                    { -4, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "amount", "Amount", true, 1, null },
                    { -3, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "accountId", "PlayerId", true, 1, null },
                    { -2, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "userId", "PlayerId", true, 1, null },
                    { -1, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "playerId", "PlayerId", true, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "MappingRules",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
