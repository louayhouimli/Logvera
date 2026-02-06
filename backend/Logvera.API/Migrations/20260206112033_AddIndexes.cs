using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logvera.API.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogEntries_ApiId",
                table: "LogEntries");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_ApiId_StatusCode",
                table: "LogEntries",
                columns: new[] { "ApiId", "StatusCode" });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_ApiId_Timestamp",
                table: "LogEntries",
                columns: new[] { "ApiId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_Apis_ApiKey",
                table: "Apis",
                column: "ApiKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogEntries_ApiId_StatusCode",
                table: "LogEntries");

            migrationBuilder.DropIndex(
                name: "IX_LogEntries_ApiId_Timestamp",
                table: "LogEntries");

            migrationBuilder.DropIndex(
                name: "IX_Apis_ApiKey",
                table: "Apis");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_ApiId",
                table: "LogEntries",
                column: "ApiId");
        }
    }
}
