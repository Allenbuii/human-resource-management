using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_TrainingPrograms_TrainingProgramId",
                table: "Instructors");

            migrationBuilder.DropIndex(
                name: "IX_Instructors_TrainingProgramId",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "TrainingProgramId",
                table: "Instructors");

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "TrainingPrograms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingPrograms_InstructorId",
                table: "TrainingPrograms",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_Instructors_InstructorId",
                table: "TrainingPrograms",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_Instructors_InstructorId",
                table: "TrainingPrograms");

            migrationBuilder.DropIndex(
                name: "IX_TrainingPrograms_InstructorId",
                table: "TrainingPrograms");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "TrainingPrograms");

            migrationBuilder.AddColumn<int>(
                name: "TrainingProgramId",
                table: "Instructors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_TrainingProgramId",
                table: "Instructors",
                column: "TrainingProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_TrainingPrograms_TrainingProgramId",
                table: "Instructors",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
