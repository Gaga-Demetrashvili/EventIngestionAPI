using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventIngestionAPI.Infrastructure.Data.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToExternalFieldCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MappingRules_ExternalField",
                table: "MappingRules",
                column: "ExternalField",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MappingRules_ExternalField",
                table: "MappingRules");
        }
    }
}
