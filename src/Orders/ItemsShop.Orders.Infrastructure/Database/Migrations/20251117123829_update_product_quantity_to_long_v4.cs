using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsShop.Orders.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class update_product_quantity_to_long_v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProductQuantity",
                schema: "orders",
                table: "OrderItems",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductQuantity",
                schema: "orders",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
