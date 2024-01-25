using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoleNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01f25097-846a-4e70-9e5c-78390b308731",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06b4a61f-fadd-4cb4-9e4a-7d8aa444d842",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Gamer", "GAMER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01f25097-846a-4e70-9e5c-78390b308731",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Writer", "WRITER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06b4a61f-fadd-4cb4-9e4a-7d8aa444d842",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Reader", "READER" });
        }
    }
}
