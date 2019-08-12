using Microsoft.EntityFrameworkCore.Migrations;

namespace MEDAPP.WebAPI.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "General");

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Dentistry");

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Pediatrics");

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Neurology");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: null);

            migrationBuilder.UpdateData(
                table: "AppointmentCategory",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: null);
        }
    }
}
