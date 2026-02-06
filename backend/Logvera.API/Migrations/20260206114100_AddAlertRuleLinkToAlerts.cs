using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logvera.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAlertRuleLinkToAlerts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlertRuleId",
                table: "Alerts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertRuleId",
                table: "Alerts");
        }
    }
}
