using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP_API.Migrations
{
    /// <inheritdoc />
    public partial class ERPCambiosCurriculum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurriculumCandidate");

            migrationBuilder.AddColumn<int>(
                name: "id_candidate_fk",
                table: "Curriculum",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculum_id_candidate_fk",
                table: "Curriculum",
                column: "id_candidate_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_Curriculum_Candidate_id_candidate_fk",
                table: "Curriculum",
                column: "id_candidate_fk",
                principalTable: "Candidate",
                principalColumn: "id_candidate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Candidate_id_candidate_fk",
                table: "Curriculum");

            migrationBuilder.DropIndex(
                name: "IX_Curriculum_id_candidate_fk",
                table: "Curriculum");

            migrationBuilder.DropColumn(
                name: "id_candidate_fk",
                table: "Curriculum");
        }
    }
}