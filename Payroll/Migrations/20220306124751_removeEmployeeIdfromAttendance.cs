using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Migrations
{
    public partial class removeEmployeeIdfromAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaries_Employees_EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeSalaries");

            migrationBuilder.DropColumn(
                name: "NoDays",
                table: "Attendances");

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "EmployeeSalaries",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoDays",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaries_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaries_Employees_EmployeeId",
                table: "EmployeeSalaries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
