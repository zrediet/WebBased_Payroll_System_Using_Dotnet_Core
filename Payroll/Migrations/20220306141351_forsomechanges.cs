using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Migrations
{
    public partial class forsomechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "EmployeeFamilies",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "EmployeeFamilies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Deductions",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Terminations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminationReason = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deductions_EmployeeId",
                table: "Deductions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_EmployeeId",
                table: "Terminations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deductions_Employees_EmployeeId",
                table: "Deductions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deductions_Employees_EmployeeId",
                table: "Deductions");

            migrationBuilder.DropTable(
                name: "Terminations");

            migrationBuilder.DropIndex(
                name: "IX_Deductions_EmployeeId",
                table: "Deductions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "EmployeeFamilies");

            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "EmployeeFamilies");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Deductions");
        }
    }
}
