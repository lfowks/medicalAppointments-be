using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEDAPP.WebAPI.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            
            migrationBuilder.InsertData(
                table: "AppointmentCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "General", "General" },
                    { 2, "Dentistry", "Dentistry" },
                    { 3, "Pediatrics", "Pediatrics" },
                    { 4, "Neurology", "Neurology" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "ADMIN", "ADMIN" },
                    { 2, "ADMIN", "USER" }
                });
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
