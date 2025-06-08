using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAdvisor.Migrations
{
    /// <inheritdoc />
    public partial class AddExpectedVisitTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ExpectedVisitTime",
                table: "Places",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedVisitTime",
                table: "Places");
        }
    }
}
