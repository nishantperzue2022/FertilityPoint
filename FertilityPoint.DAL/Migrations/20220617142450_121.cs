using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityPoint.DAL.Migrations
{
    public partial class _121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "TimeSlots",
                newName: "ToTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromTime",
                table: "TimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "TimeSlots");

            migrationBuilder.RenameColumn(
                name: "ToTime",
                table: "TimeSlots",
                newName: "Time");
        }
    }
}
