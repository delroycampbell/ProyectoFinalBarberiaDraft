using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalDraft.Migrations
{
    /// <inheritdoc />
    public partial class FixFacturaFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_CitaId",
                table: "Factura");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Factura",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factura_UsuarioId",
                table: "Factura",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_UsuarioId1",
                table: "Factura",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Usuario_UsuarioId",
                table: "Factura",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Usuario_UsuarioId1",
                table: "Factura",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_UsuarioId",
                table: "Factura");

            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_UsuarioId1",
                table: "Factura");

            migrationBuilder.DropIndex(
                name: "IX_Factura_UsuarioId",
                table: "Factura");

            migrationBuilder.DropIndex(
                name: "IX_Factura_UsuarioId1",
                table: "Factura");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Factura");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Usuario_CitaId",
                table: "Factura",
                column: "CitaId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
