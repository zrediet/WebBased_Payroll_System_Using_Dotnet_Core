using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Migrations
{
    public partial class addPayrollSheetModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayrollSheets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    PayStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasicSalary = table.Column<float>(type: "real", nullable: false),
                    Allowance = table.Column<float>(type: "real", nullable: false),
                    OverTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossSalary = table.Column<float>(type: "real", nullable: false),
                    IncomeTax = table.Column<float>(type: "real", nullable: false),
                    PensionEmployee = table.Column<float>(type: "real", nullable: false),
                    PensionCompany = table.Column<float>(type: "real", nullable: false),
                    Loan = table.Column<float>(type: "real", nullable: false),
                    Penality = table.Column<float>(type: "real", nullable: false),
                    OtherDeduction = table.Column<float>(type: "real", nullable: true),
                    NetPay = table.Column<float>(type: "real", nullable: false),
                    NonTaxableAllowance = table.Column<float>(type: "real", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollSheets_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSheets_EmployeeId",
                table: "PayrollSheets",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayrollSheets");
        }
    }
}
