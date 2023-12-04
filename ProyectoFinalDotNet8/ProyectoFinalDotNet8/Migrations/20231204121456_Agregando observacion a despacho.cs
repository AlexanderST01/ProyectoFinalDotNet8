using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalDotNet8.Migrations
{
    /// <inheritdoc />
    public partial class Agregandoobservacionadespacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Despachos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDistrital",
                table: "CentrosEducativos",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Despachos");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoDistrital",
                table: "CentrosEducativos",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
