using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Academic.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleSectionUsers_IdentityUsers_UsersId",
                table: "ModuleSectionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleUsers_IdentityUsers_UsersId",
                table: "ModuleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PathTaskUsers_IdentityUsers_UsersId",
                table: "PathTaskUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PathUsers_IdentityUsers_UsersId",
                table: "PathUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PathUsers_Paths_PathsId",
                table: "PathUsers");

            migrationBuilder.DropIndex(
                name: "IX_PathUsers_PathsId",
                table: "PathUsers");

            migrationBuilder.DropIndex(
                name: "IX_PathUsers_UsersId",
                table: "PathUsers");

            migrationBuilder.DropIndex(
                name: "IX_PathTaskUsers_UsersId",
                table: "PathTaskUsers");

            migrationBuilder.DropIndex(
                name: "IX_ModuleUsers_UsersId",
                table: "ModuleUsers");

            migrationBuilder.DropIndex(
                name: "IX_ModuleSectionUsers_UsersId",
                table: "ModuleSectionUsers");

            migrationBuilder.DropColumn(
                name: "PathsId",
                table: "PathUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "PathUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "PathTaskUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "ModuleUsers");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "ModuleSectionUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IdentityUsers",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "IdentityUsers",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IdentityUsers");

            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "IdentityUsers");

            migrationBuilder.AddColumn<int>(
                name: "PathsId",
                table: "PathUsers",
                type: "int(8)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "PathUsers",
                type: "int(8)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "PathTaskUsers",
                type: "int(8)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "ModuleUsers",
                type: "int(8)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "ModuleSectionUsers",
                type: "int(8)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PathUsers_PathsId",
                table: "PathUsers",
                column: "PathsId");

            migrationBuilder.CreateIndex(
                name: "IX_PathUsers_UsersId",
                table: "PathUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PathTaskUsers_UsersId",
                table: "PathTaskUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleUsers_UsersId",
                table: "ModuleUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleSectionUsers_UsersId",
                table: "ModuleSectionUsers",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleSectionUsers_IdentityUsers_UsersId",
                table: "ModuleSectionUsers",
                column: "UsersId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleUsers_IdentityUsers_UsersId",
                table: "ModuleUsers",
                column: "UsersId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PathTaskUsers_IdentityUsers_UsersId",
                table: "PathTaskUsers",
                column: "UsersId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PathUsers_IdentityUsers_UsersId",
                table: "PathUsers",
                column: "UsersId",
                principalTable: "IdentityUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PathUsers_Paths_PathsId",
                table: "PathUsers",
                column: "PathsId",
                principalTable: "Paths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
