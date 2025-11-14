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
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
