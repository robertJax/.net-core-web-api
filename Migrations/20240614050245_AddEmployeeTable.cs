using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sampleaapi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EmployeeName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime(6)", maxLength: 200, nullable: false),
                    Island = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    NationalId = table.Column<int>(type: "int", maxLength: 500, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmpId", "DOB", "DepartmentId", "EmployeeName", "Island", "NationalId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jackson Robert", "Efate", 111 },
                    { 2, new DateTime(2021, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tchilumba Mera", "Maewo", 112 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
