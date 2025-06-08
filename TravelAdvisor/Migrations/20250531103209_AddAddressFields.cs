using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAdvisor.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Places");
        }
    }
}
