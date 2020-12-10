using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeMeRich.Infrastructure.Persistance.Migrations
{
    public partial class RemoveInternalTransactionCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalTransactionCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalTransactionCategories",
                columns: table => new
                {
                    InternalTransactionId = table.Column<int>(type: "int", nullable: false),
                    FinancialCategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalTransactionCategories", x => new { x.InternalTransactionId, x.FinancialCategoryId });
                    table.ForeignKey(
                        name: "FK_InternalTransactionCategories_FinancialCategories_FinancialCategoryId",
                        column: x => x.FinancialCategoryId,
                        principalTable: "FinancialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalTransactionCategories_InternalTransactions_InternalTransactionId",
                        column: x => x.InternalTransactionId,
                        principalTable: "InternalTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactionCategories_FinancialCategoryId",
                table: "InternalTransactionCategories",
                column: "FinancialCategoryId");
        }
    }
}
