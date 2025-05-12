using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerFullName145645 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage");

            migrationBuilder.AddColumn<string>(
                name: "DescAr",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescEn",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyRequestId",
                table: "PropertyRequestImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage",
                column: "PropertyRequestId",
                principalTable: "PropertyRequests",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage");

            migrationBuilder.DropColumn(
                name: "DescAr",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "DescEn",
                table: "PropertyRequests");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyRequestId",
                table: "PropertyRequestImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequestImage_PropertyRequests_PropertyRequestId",
                table: "PropertyRequestImage",
                column: "PropertyRequestId",
                principalTable: "PropertyRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
