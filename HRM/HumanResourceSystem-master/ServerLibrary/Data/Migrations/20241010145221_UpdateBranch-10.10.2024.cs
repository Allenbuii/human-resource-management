using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBranch10102024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfEmployees",
                table: "Branches",
                newName: "RequiredEmployees");

            migrationBuilder.AddColumn<int>(
                name: "CurrentEmployees",
                table: "Branches",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentEmployees",
                table: "Branches");

            migrationBuilder.RenameColumn(
                name: "RequiredEmployees",
                table: "Branches",
                newName: "NumberOfEmployees");
        }
    }
}
