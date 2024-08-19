using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Records_Master.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patient",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Patient",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patient",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }
    }
}
