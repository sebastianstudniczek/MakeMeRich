using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeMeRich.Infrastructure.Persistance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AccountType = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionSideName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialAccountId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTransactions_FinancialAccounts_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SendingAccountId = table.Column<int>(type: "int", nullable: false),
                    ReceivingAccountId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalTransactions", x => x.Id);
                    table.CheckConstraint("CHK_InternalTransactions_SendingAccountId", "SendingAccountId != ReceivingAccountId");
                    table.CheckConstraint("CHK_InternalTransactions_ReceivingAccountId", "ReceivingAccountId != SendingAccountId");
                    table.ForeignKey(
                        name: "FK_InternalTransactions_FinancialAccounts_ReceivingAccountId",
                        column: x => x.ReceivingAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternalTransactions_FinancialAccounts_SendingAccountId",
                        column: x => x.SendingAccountId,
                        principalTable: "FinancialAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalTransactionCategories",
                columns: table => new
                {
                    FinancialCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExternalTransactionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransactionCategories", x => new { x.ExternalTransactionId, x.FinancialCategoryId });
                    table.ForeignKey(
                        name: "FK_ExternalTransactionCategories_ExternalTransactions_ExternalTransactionId",
                        column: x => x.ExternalTransactionId,
                        principalTable: "ExternalTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalTransactionCategories_FinancialCategories_FinancialCategoryId",
                        column: x => x.FinancialCategoryId,
                        principalTable: "FinancialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalTransactionCategories",
                columns: table => new
                {
                    FinancialCategoryId = table.Column<int>(type: "int", nullable: false),
                    InternalTransactionId = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_ExternalTransactionCategories_FinancialCategoryId",
                table: "ExternalTransactionCategories",
                column: "FinancialCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTransactions_FinancialAccountId",
                table: "ExternalTransactions",
                column: "FinancialAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactionCategories_FinancialCategoryId",
                table: "InternalTransactionCategories",
                column: "FinancialCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactions_ReceivingAccountId",
                table: "InternalTransactions",
                column: "ReceivingAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactions_SendingAccountId",
                table: "InternalTransactions",
                column: "SendingAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalTransactionCategories");

            migrationBuilder.DropTable(
                name: "InternalTransactionCategories");

            migrationBuilder.DropTable(
                name: "ExternalTransactions");

            migrationBuilder.DropTable(
                name: "FinancialCategories");

            migrationBuilder.DropTable(
                name: "InternalTransactions");

            migrationBuilder.DropTable(
                name: "FinancialAccounts");
        }
    }
}
