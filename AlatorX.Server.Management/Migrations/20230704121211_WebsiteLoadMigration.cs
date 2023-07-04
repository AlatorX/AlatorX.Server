using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlatorX.Server.Management.Migrations
{
    /// <inheritdoc />
    public partial class WebsiteLoadMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWebsites_Websites_WebsiteId",
                table: "UserWebsites");

            migrationBuilder.RenameColumn(
                name: "WebsiteId",
                table: "UserWebsites",
                newName: "WebsiteSettingId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWebsites_WebsiteId",
                table: "UserWebsites",
                newName: "IX_UserWebsites_WebsiteSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWebsites_WebsiteSettings_WebsiteSettingId",
                table: "UserWebsites",
                column: "WebsiteSettingId",
                principalTable: "WebsiteSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWebsites_WebsiteSettings_WebsiteSettingId",
                table: "UserWebsites");

            migrationBuilder.RenameColumn(
                name: "WebsiteSettingId",
                table: "UserWebsites",
                newName: "WebsiteId");

            migrationBuilder.RenameIndex(
                name: "IX_UserWebsites_WebsiteSettingId",
                table: "UserWebsites",
                newName: "IX_UserWebsites_WebsiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWebsites_Websites_WebsiteId",
                table: "UserWebsites",
                column: "WebsiteId",
                principalTable: "Websites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
