using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FertilityPoint.DAL.Migrations
{
    public partial class _7844 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaybillPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessShortCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillRefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgAccountBalance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdPartyTransID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MSISDN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaybillPayments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaybillPayments");
        }
    }
}
