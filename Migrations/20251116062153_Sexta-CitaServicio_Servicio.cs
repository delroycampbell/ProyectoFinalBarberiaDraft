using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalDraft.Migrations
{
    /// <inheritdoc />
    public partial class SextaCitaServicio_Servicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_UsuarioId",
                table: "Factura");

            migrationBuilder.DropIndex(
                name: "IX_Factura_UsuarioId",
                table: "Factura");

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.ServicioId);
                });

            migrationBuilder.CreateTable(
                name: "CitaServicio",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaServicio", x => new { x.CitaId, x.ServicioId });
                    table.ForeignKey(
                        name: "FK_CitaServicio_Cita_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Cita",
                        principalColumn: "CitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitaServicio_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitaServicio_ServicioId",
                table: "CitaServicio",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factura_Usuario_CitaId",
                table: "Factura",
                column: "CitaId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factura_Usuario_CitaId",
                table: "Factura");

            migrationBuilder.DropTable(
                name: "CitaServicio");

            migrationBuilder.DropTable(
                name: "Servicio");

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
    }
}
