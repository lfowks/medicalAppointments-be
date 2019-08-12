using Microsoft.EntityFrameworkCore.Migrations;

namespace MEDAPP.WebAPI.Migrations
{
    public partial class Seed1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Description", "Name" },
                values: new object[] { 1, null, "ADMIN" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Description", "Name" },
                values: new object[] { 2, null, "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: 2);
        }
    }
}
