using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MeasurementUnitId",
                schema: "inventory",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_products_bar_code",
                schema: "inventory",
                table: "products",
                column: "bar_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_measurement_units_abbreviation",
                schema: "inventory",
                table: "measurement_units",
                column: "abbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_measurement_units_name",
                schema: "inventory",
                table: "measurement_units",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_bar_code",
                schema: "inventory",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_measurement_units_abbreviation",
                schema: "inventory",
                table: "measurement_units");

            migrationBuilder.DropIndex(
                name: "IX_measurement_units_name",
                schema: "inventory",
                table: "measurement_units");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                schema: "inventory",
                table: "products");
        }
    }
}
