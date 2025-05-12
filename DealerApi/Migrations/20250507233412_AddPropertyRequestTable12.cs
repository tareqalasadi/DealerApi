using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DealerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyRequestTable12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBalcony",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "PropertyRequests");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PropertyRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PropertyRequests");

            migrationBuilder.AddColumn<bool>(
                name: "HasBalcony",
                table: "PropertyRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
