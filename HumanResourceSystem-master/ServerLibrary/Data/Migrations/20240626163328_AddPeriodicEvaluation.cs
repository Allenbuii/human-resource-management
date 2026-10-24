using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodicEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeriodicEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EvaluationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TechnicalSkillsScore = table.Column<int>(type: "int", nullable: false),
                    CommunicationSkillsScore = table.Column<int>(type: "int", nullable: false),
                    TeamworkSkillsScore = table.Column<int>(type: "int", nullable: false),
                    ProblemSolvingSkillsScore = table.Column<int>(type: "int", nullable: false),
                    WorkEthicScore = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodicEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodicEvaluations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodicEvaluations_EmployeeId",
                table: "PeriodicEvaluations",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeriodicEvaluations");
        }
    }
}
