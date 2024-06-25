using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace XpCase.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IniteCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "assets",
                columns: table => new
                {
                    asset_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    types = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assets", x => x.asset_id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    asset_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    is_buy_order = table.Column<bool>(type: "bit", maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_orders_assets_asset_id",
                        column: x => x.asset_id,
                        principalTable: "assets",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_transactions_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "wallets",
                columns: table => new
                {
                    wallet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    asset_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.wallet_id);
                    table.ForeignKey(
                        name: "FK_wallets_assets_asset_id",
                        column: x => x.asset_id,
                        principalTable: "assets",
                        principalColumn: "asset_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_wallets_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "accounts",
                columns: new[] { "account_id", "created_at", "email", "name" },
                values: new object[,]
                {
                    { new Guid("54a2a569-905a-4a72-b53c-c5e3344f8556"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8216), "maria@operacao.com", "Maria da Silva" },
                    { new Guid("9f2c64ba-cdc5-4012-809d-e60478fc4fb6"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8230), "jose@operacao.com", "José da Silva" },
                    { new Guid("f291ec66-8d25-4533-912a-020f055e9b75"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8200), "joao@operacao.com", "João da Silva" }
                });

            migrationBuilder.InsertData(
                table: "assets",
                columns: new[] { "asset_id", "created_at", "expiration_date", "name", "price", "quantity", "symbol", "types" },
                values: new object[,]
                {
                    { new Guid("1c0efd94-3b9c-4926-b6ba-c9f301ed1782"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8354), new DateTime(2027, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8354), "Fundo Imobiliário ABC", 150.00m, 50, "FIABC", "Fundos de Investimentos" },
                    { new Guid("b5da053f-63af-4d83-bbf5-8f3532b37f70"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8350), new DateTime(2027, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8349), "Tesouro Selic 2025", 50m, 200, "TS2025", "Tesouro Direto" },
                    { new Guid("ce9455e5-cc39-4547-8221-c5d2aec32ac5"), new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8346), new DateTime(2029, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8337), "Ação XYZ", 100m, 100, "ACXYZ", "Ações" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "customer_id", "amount", "created_at", "email", "name" },
                values: new object[,]
                {
                    { new Guid("030270cf-a15f-4091-a46d-ce13fb552f7c"), 10000.00m, new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8386), "ana@customer.com", "Ana dos Santos" },
                    { new Guid("8bb1ab75-3060-492e-802d-368d577de3b0"), 5000.00m, new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8383), "ricardo@customer.com", "Ricardo dos Santos" },
                    { new Guid("df033a27-90f5-4276-9f93-9389a8078c31"), 300.00m, new DateTime(2024, 6, 17, 18, 53, 43, 881, DateTimeKind.Local).AddTicks(8388), "felipe@customer.com", "Felipe dos Santos" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_email",
                table: "accounts",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_assets_symbol",
                table: "assets",
                column: "symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_asset_id",
                table: "orders",
                column: "asset_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_customer_id",
                table: "transactions",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_wallets_asset_id_customer_id",
                table: "wallets",
                columns: new[] { "asset_id", "customer_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wallets_customer_id",
                table: "wallets",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "wallets");

            migrationBuilder.DropTable(
                name: "assets");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
