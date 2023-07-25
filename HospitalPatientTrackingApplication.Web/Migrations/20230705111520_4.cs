using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalPatientTrackingApplication.Web.Migrations
{
    /// <inheritdoc />
    public partial class _4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_id_card",
                table: "Patients",
                column: "id_card",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_id_card",
                table: "Patients");
        }
    }
}
