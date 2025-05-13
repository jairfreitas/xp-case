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
                    { new Guid("2ffae86a-d19c-419a-a02f-5fc992b89ddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jose@operacao.com", "José da Silva" },
                    { new Guid("8362097d-fa43-479e-83a5-f8a91f09f2ec"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria@operacao.com", "Maria da Silva" },
                    { new Guid("88b2a625-41b7-4a5f-a184-84dbad26cc7d"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao@operacao.com", "João da Silva" }
                });

            migrationBuilder.InsertData(
                table: "assets",
                columns: new[] { "asset_id", "created_at", "expiration_date", "name", "price", "quantity", "symbol", "types" },
                values: new object[,]
                {
                    { new Guid("b102b62f-aacb-4c10-a839-806699de8781"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tesouro Selic 2025", 50m, 200, "TS2025", "Tesouro Direto" },
                    { new Guid("f1584129-1b96-4e0e-a46c-9459260e0d2e"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ação XYZ", 100m, 100, "ACXYZ", "Ações" },
                    { new Guid("f53328fe-b322-4182-8cc3-8d883c8e6a78"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fundo Imobiliário ABC", 150.00m, 50, "FIABC", "Fundos de Investimentos" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "customer_id", "amount", "created_at", "email", "name" },
                values: new object[,]
                {
                    { new Guid("118afd34-cb8b-4039-b76f-148759e47374"), 5000.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ricardo@customer.com", "Ricardo dos Santos" },
                    { new Guid("13e5ce20-9ddc-4502-9527-9a9be0b94c45"), 10000.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ana@customer.com", "Ana dos Santos" },
                    { new Guid("1934592b-0942-4876-9d57-8253c0c30fec"), 300.00m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felipe@customer.com", "Felipe dos Santos" }
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
