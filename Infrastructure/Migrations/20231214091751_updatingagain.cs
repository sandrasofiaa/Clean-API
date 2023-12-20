using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "AnimalModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dog_Breed",
                table: "AnimalModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dog_Weight",
                table: "AnimalModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AnimalModels",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "AnimalModels");

            migrationBuilder.DropColumn(
                name: "Dog_Breed",
                table: "AnimalModels");

            migrationBuilder.DropColumn(
                name: "Dog_Weight",
                table: "AnimalModels");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AnimalModels");
        }
    }
}
