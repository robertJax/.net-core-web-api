using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sampleaapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DepartmentName", "Description" },
                values: new object[] { "Customs", "Customs Department" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DepartmentName", "Description" },
                values: new object[] { "Finance and Treasury", "Finance Department" });
        }
    }
}
