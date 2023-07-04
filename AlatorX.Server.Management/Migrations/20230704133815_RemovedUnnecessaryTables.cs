using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AlatorX.Server.Management.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnnecessaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWebsites");

            migrationBuilder.DropTable(
                name: "WebsiteSettings");

            migrationBuilder.AddColumn<string>(
                name: "ConfigString",
                table: "Websites",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Websites",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Websites_UserId",
                table: "Websites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Websites_Users_UserId",
                table: "Websites");

            migrationBuilder.DropIndex(
                name: "IX_Websites_UserId",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "ConfigString",
                table: "Websites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Websites");

            migrationBuilder.CreateTable(
                name: "WebsiteSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WebsiteId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigString = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteSettings_Websites_WebsiteId",
                        column: x => x.WebsiteId,
                        principalTable: "Websites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWebsites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    WebsiteSettingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWebsites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWebsites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWebsites_WebsiteSettings_WebsiteSettingId",
                        column: x => x.WebsiteSettingId,
                        principalTable: "WebsiteSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWebsites_UserId",
                table: "UserWebsites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWebsites_WebsiteSettingId",
                table: "UserWebsites",
                column: "WebsiteSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteSettings_WebsiteId",
                table: "WebsiteSettings",
                column: "WebsiteId");
        }
    }
}
