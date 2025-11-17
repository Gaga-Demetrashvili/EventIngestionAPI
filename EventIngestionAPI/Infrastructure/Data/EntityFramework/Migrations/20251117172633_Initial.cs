using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MappingRuleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingRuleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MappingRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InternalField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MappingRuleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MappingRules_MappingRuleTypes_MappingRuleTypeId",
                        column: x => x.MappingRuleTypeId,
                        principalTable: "MappingRuleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MappingRuleTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "default" },
                    { 2, "dynamic" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MappingRules_ExternalField",
                table: "MappingRules",
                column: "ExternalField",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MappingRules_MappingRuleTypeId",
                table: "MappingRules",
                column: "MappingRuleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MappingRules");

            migrationBuilder.DropTable(
                name: "MappingRuleTypes");
        }
    }
}
