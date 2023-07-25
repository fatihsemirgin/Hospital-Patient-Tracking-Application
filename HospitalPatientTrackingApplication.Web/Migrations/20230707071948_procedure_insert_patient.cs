using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalPatientTrackingApplication.Web.Migrations
{
    /// <inheritdoc />
    public partial class procedure_insert_patient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var stored_p = "CREATE OR REPLACE PROCEDURE " +
                "insert_patient\r\n(\r\np_id_card\tbigint,\r\np_name_surname\tvarchar(40),\r\np_birth_date\tdate\r\n)\r\nAS $$\r\n" +
                "BEGIN\r\nif p_id_card is NULL then \r\n\tRAISE EXCEPTION 'id_car must be unique and must have a value';\r\n\tend if;\r\nif p_id_card is NULL  then \r\n\t" +
                "RAISE EXCEPTION 'id_car must have a value';\r\n\tend if;\r\nif p_id_card is NULL then \r\n\tRAISE EXCEPTION 'id_car must have a value';\r\n\tend if;\r\n" +
                "INSERT INTO \"Patients\"(id_card,\r\nname_surname,birth_date)\r\nVALUES (p_id_card,p_name_surname,p_birth_date);\r\nEND;\r\n$$ LANGUAGE plpgsql\t\r\n";

            migrationBuilder.Sql(stored_p);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
