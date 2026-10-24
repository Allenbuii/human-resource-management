using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtime_OvertimeType_OvertimeTypeId",
                table: "Overtime");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationsType_VacationTypeId",
                table: "Vacations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationsType",
                table: "VacationsType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeType",
                table: "OvertimeType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Overtime",
                table: "Overtime");

            migrationBuilder.RenameTable(
                name: "VacationsType",
                newName: "VacationsTypes");

            migrationBuilder.RenameTable(
                name: "OvertimeType",
                newName: "OvertimeTypes");

            migrationBuilder.RenameTable(
                name: "Overtime",
                newName: "Overtimes");

            migrationBuilder.RenameIndex(
                name: "IX_Overtime_OvertimeTypeId",
                table: "Overtimes",
                newName: "IX_Overtimes_OvertimeTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationsTypes",
                table: "VacationsTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Overtimes",
                table: "Overtimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtimes_OvertimeTypes_OvertimeTypeId",
                table: "Overtimes",
                column: "OvertimeTypeId",
                principalTable: "OvertimeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationsTypes_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId",
                principalTable: "VacationsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Overtimes_OvertimeTypes_OvertimeTypeId",
                table: "Overtimes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_VacationsTypes_VacationTypeId",
                table: "Vacations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VacationsTypes",
                table: "VacationsTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeTypes",
                table: "OvertimeTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Overtimes",
                table: "Overtimes");

            migrationBuilder.RenameTable(
                name: "VacationsTypes",
                newName: "VacationsType");

            migrationBuilder.RenameTable(
                name: "OvertimeTypes",
                newName: "OvertimeType");

            migrationBuilder.RenameTable(
                name: "Overtimes",
                newName: "Overtime");

            migrationBuilder.RenameIndex(
                name: "IX_Overtimes_OvertimeTypeId",
                table: "Overtime",
                newName: "IX_Overtime_OvertimeTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VacationsType",
                table: "VacationsType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeType",
                table: "OvertimeType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Overtime",
                table: "Overtime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Overtime_OvertimeType_OvertimeTypeId",
                table: "Overtime",
                column: "OvertimeTypeId",
                principalTable: "OvertimeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_VacationsType_VacationTypeId",
                table: "Vacations",
                column: "VacationTypeId",
                principalTable: "VacationsType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
