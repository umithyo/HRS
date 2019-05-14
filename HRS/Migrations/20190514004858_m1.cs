using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRS.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
           
                          
            migrationBuilder.CreateIndex(
                name: "IX_Polyclinics_HospitalId",
                table: "Polyclinics",
                column: "HospitalId");

           
            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_ClinicId",
                table: "UserInfos",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_HospitalId",
                table: "UserInfos",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserInfoId",
                table: "Users",
                column: "UserInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "HospitalClinic");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Polyclinics");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropTable(
                name: "Clinics");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
