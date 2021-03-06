using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityPoint.DAL.Migrations
{
    public partial class _61 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TimeId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Appointments");
        }
    }
}
