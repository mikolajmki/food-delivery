using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace food_delivery.Migrations
{
    public partial class price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
