using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatett11123145 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyRequestId",
                table: "PropertyRequestImage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRequestImage_PropertyRequestId",
                table: "PropertyRequestImage",
                column: "PropertyRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage",
                column: "PropertyRequestId",
                principalTable: "PropertyRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage");

            migrationBuilder.DropIndex(
                name: "IX_PropertyRequestImage_PropertyRequestId",
                table: "PropertyRequestImage");

            migrationBuilder.DropColumn(
                name: "PropertyRequestId",
                table: "PropertyRequestImage");
        }
    }
}
