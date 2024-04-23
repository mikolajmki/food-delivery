using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace food_delivery.Migrations
{
    public partial class review_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_OrderDetails_OrderDetailsId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_OrderDetailsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "OrderDetailsId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Items_ItemId",
                table: "Reviews",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Items_ItemId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailsId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderDetailsId",
                table: "Reviews",
                column: "OrderDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_OrderDetails_OrderDetailsId",
                table: "Reviews",
                column: "OrderDetailsId",
                principalTable: "OrderDetails",
                principalColumn: "Id");
        }
    }
}
