using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable //`test

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

            migrationBuilder.CreateIndex(
                name: "IX_Factura_UsuarioId",
                table: "Factura",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Usuario_UsuarioId",
                table: "Factura",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_UsuarioId",
                table: "Factura");

            migrationBuilder.DropIndex(
                name: "IX_Factura_UsuarioId",
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
