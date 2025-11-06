using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsShop.Orders.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class update_order_status_type_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "orders",
                table: "Orders",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "orders",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldDefaultValue: (byte)0);
        }
    }
}
