using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Курсач.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menuitemcategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuitemcategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "orderstatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderstatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "userroles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userroles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "menuitems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menuitems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_menuitems_menuitemcategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "menuitemcategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    DOB = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_users_userroles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "userroles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_orders_orderstatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "orderstatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableNumber = table.Column<int>(type: "integer", nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    IsReserved = table.Column<bool>(type: "boolean", nullable: false),
                    ReservedByUserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.TableId);
                    table.ForeignKey(
                        name: "FK_tables_users_ReservedByUserId",
                        column: x => x.ReservedByUserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderitems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    MenuItemId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ItemPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderitems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_orderitems_menuitems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "menuitems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orderitems_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_payments_orders_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    CustomerFirstName = table.Column<string>(type: "text", nullable: false),
                    CustomerLastName = table.Column<string>(type: "text", nullable: false),
                    ReservationDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TablesTableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_reservations_tables_TableId",
                        column: x => x.TableId,
                        principalTable: "tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_tables_TablesTableId",
                        column: x => x.TablesTableId,
                        principalTable: "tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "menuitemcategories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Напитки" },
                    { 2, "Десерты" }
                });

            migrationBuilder.InsertData(
                table: "orderstatuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "В обработке" },
                    { 2, "Готовиться" },
                    { 3, "Готов" },
                    { 4, "Возврат" }
                });

            migrationBuilder.InsertData(
                table: "userroles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Пользователь" },
                    { 2, "Админ" },
                    { 3, "Сотрудник" }
                });

            migrationBuilder.InsertData(
                table: "menuitems",
                columns: new[] { "MenuItemId", "Availability", "CategoryId", "Description", "ItemName", "Price" },
                values: new object[,]
                {
                    { 1, true, 1, "Нежный латте с великолепным ароматом", "Кофе Латте", 99m },
                    { 2, true, 1, "Ароматный капучино с пышной пенкой", "Капучино", 89m },
                    { 3, true, 1, "Крепкий американо с насыщенным вкусом", "Американо", 69m },
                    { 4, true, 1, "Классическое эспрессо для настоящих ценителей", "Эспрессо", 89m },
                    { 5, true, 1, "Восхитительный мокко с добавлением шоколада", "Мокко", 100m },
                    { 6, true, 2, "Нежный чизкейк с сочными ягодами", "Чизкейк", 120m },
                    { 7, true, 2, "Ароматный круассан с копчёным лососем", "Круассан с лососем", 100m },
                    { 8, true, 2, "Свежий фруктовый салат с мятным соусом", "Фруктовый салат", 150m },
                    { 9, true, 2, "Панкейки с кленовым сиропом", "Панкейки с кленовым сиропом", 200m }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserId", "CreatedAt", "DOB", "FirstName", "LastName", "Password", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 25, 13, 34, 59, 575, DateTimeKind.Local).AddTicks(7850), "01/01/1990", "Daniel", "Met", "123", "89999999999", 2, "admin" },
                    { 2, new DateTime(2023, 11, 25, 13, 34, 59, 575, DateTimeKind.Local).AddTicks(7858), "01/01/1990", "Max", "Met", "123", "89771484774", 3, "sotryd" }
                });

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "TableId", "Capacity", "IsReserved", "ReservedByUserId", "TableNumber" },
                values: new object[,]
                {
                    { 1, 4, false, 1, 1 },
                    { 2, 4, false, 1, 2 },
                    { 3, 4, false, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_menuitems_CategoryId",
                table: "menuitems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_MenuItemId",
                table: "orderitems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_OrderId",
                table: "orderitems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_StatusId",
                table: "orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_TableId",
                table: "reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_TablesTableId",
                table: "reservations",
                column: "TablesTableId");

            migrationBuilder.CreateIndex(
                name: "IX_tables_ReservedByUserId",
                table: "tables",
                column: "ReservedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderitems");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "menuitems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "menuitemcategories");

            migrationBuilder.DropTable(
                name: "orderstatuses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "userroles");
        }
    }
}
