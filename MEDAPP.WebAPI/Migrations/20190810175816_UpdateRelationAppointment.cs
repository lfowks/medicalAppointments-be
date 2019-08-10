using Microsoft.EntityFrameworkCore.Migrations;

namespace MEDAPP.WebAPI.Migrations
{
    public partial class UpdateRelationAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointment_AppointmentCategoryId",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_AppointmentCategoryId",
                table: "Appointment",
                column: "AppointmentCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointment_AppointmentCategoryId",
                table: "Appointment");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_AppointmentCategoryId",
                table: "Appointment",
                column: "AppointmentCategoryId",
                unique: true);
        }
    }
}
