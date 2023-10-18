using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AccountBearerType = table.Column<int>(type: "int", nullable: false),
                    AccountTransactionLimitCurrency = table.Column<string>(name: "AccountTransactionLimit_Currency", type: "nvarchar(6)", maxLength: 6, nullable: false),
                    AccountTransactionLimitAmount = table.Column<decimal>(name: "AccountTransactionLimit_Amount", type: "decimal(18,4)", nullable: false),
                    AccountTransactionLimitTime = table.Column<DateTime>(name: "AccountTransactionLimit_Time", type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TxnTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditCurrency = table.Column<string>(name: "Credit_Currency", type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreditAmount = table.Column<decimal>(name: "Credit_Amount", type: "decimal(18,4)", nullable: false),
                    CreditTime = table.Column<DateTime>(name: "Credit_Time", type: "datetime2", nullable: false),
                    DebitCurrency = table.Column<string>(name: "Debit_Currency", type: "nvarchar(6)", maxLength: 6, nullable: false),
                    DebitAmount = table.Column<decimal>(name: "Debit_Amount", type: "decimal(18,4)", nullable: false),
                    DebitTime = table.Column<DateTime>(name: "Debit_Time", type: "datetime2", nullable: false),
                    TxnRefrence = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Narration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransactingUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ledgers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_AccountId",
                table: "Ledgers",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ledgers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
