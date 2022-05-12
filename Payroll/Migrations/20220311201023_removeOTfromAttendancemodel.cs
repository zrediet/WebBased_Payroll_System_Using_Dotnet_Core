using Microsoft.EntityFrameworkCore.Migrations;

namespace Payroll.Migrations
{
    public partial class removeOTfromAttendancemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolyDayOT",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "NoDays",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "NormalOT",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "NormalOT2",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "WeekendOT",
                table: "Attendances",
                newName: "AttendanceType");

            migrationBuilder.AddColumn<int>(
                name: "Reason",
                table: "Attendances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "AttendanceType",
                table: "Attendances",
                newName: "WeekendOT");

            migrationBuilder.AddColumn<int>(
                name: "HolyDayOT",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoDays",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormalOT",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormalOT2",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
