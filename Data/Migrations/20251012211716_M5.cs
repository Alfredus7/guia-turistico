using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guia_turistico.Data.Migrations
{
    /// <inheritdoc />
    public partial class M5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Tipos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tipos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "DescripcionIngles",
                table: "Tipos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescripcionPortugues",
                table: "Tipos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreIngles",
                table: "Tipos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePortugues",
                table: "Tipos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreIngles",
                table: "SitiosTuristicos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombrePortugues",
                table: "SitiosTuristicos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionIngles",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "DescripcionPortugues",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "NombreIngles",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "NombrePortugues",
                table: "Tipos");

            migrationBuilder.DropColumn(
                name: "NombreIngles",
                table: "SitiosTuristicos");

            migrationBuilder.DropColumn(
                name: "NombrePortugues",
                table: "SitiosTuristicos");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Tipos",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tipos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
