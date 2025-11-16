using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalDraft.Migrations
{
    /// <inheritdoc />
    public partial class EntidadesCitaEstadoCitaFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadoCita",
                columns: table => new
                {
                    EstadoCitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCita", x => x.EstadoCitaId);
                });

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstadoCitaId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.CitaId);
                    table.ForeignKey(
                        name: "FK_Cita_EstadoCita_EstadoCitaId",
                        column: x => x.EstadoCitaId,
                        principalTable: "EstadoCita",
                        principalColumn: "EstadoCitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Total = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CitaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Factura_Cita_CitaId",
                        column: x => x.CitaId,
                        principalTable: "Cita",
                        principalColumn: "CitaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factura_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EstadoCitaId",
                table: "Cita",
                column: "EstadoCitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_UsuarioId",
                table: "Cita",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_CitaId",
                table: "Factura",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factura_UsuarioId",
                table: "Factura",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "EstadoCita");
        }
    }
}
