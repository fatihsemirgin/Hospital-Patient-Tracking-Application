using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalPatientTrackingApplication.Web.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Patient_Name",
                table: "Visits");

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Name",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complaint",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "Visits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "name_surname",
                table: "Patients",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complaint",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "Visits");

            migrationBuilder.AlterColumn<string>(
                name: "Doctor_Name",
                table: "Visits",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Patient_Name",
                table: "Visits",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name_surname",
                table: "Patients",
                type: "character varying(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);
        }
    }
}
