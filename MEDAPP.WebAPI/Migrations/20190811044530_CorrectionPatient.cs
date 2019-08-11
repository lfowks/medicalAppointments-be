using Microsoft.EntityFrameworkCore.Migrations;

namespace MEDAPP.WebAPI.Migrations
{
    public partial class CorrectionPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adddress",
                table: "Patient",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Patient",
                newName: "Adddress");
        }
    }
}
