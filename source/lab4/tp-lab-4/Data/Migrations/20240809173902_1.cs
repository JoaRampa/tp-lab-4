using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp_lab_4.Data.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "afiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_afiliados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    afiliadoId = table.Column<int>(type: "int", nullable: false),
                    fechaSolicitud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    descripcion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_afiliados_afiliadoId",
                        column: x => x.afiliadoId,
                        principalTable: "afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketsId = table.Column<int>(type: "int", nullable: false),
                    descripcionPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estados = table.Column<bool>(type: "bit", nullable: false),
                    fechaEstado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketDetalles_tickets_ticketsId",
                        column: x => x.ticketsId,
                        principalTable: "tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetalles_ticketsId",
                table: "TicketDetalles",
                column: "ticketsId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_afiliadoId",
                table: "tickets",
                column: "afiliadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estados");

            migrationBuilder.DropTable(
                name: "TicketDetalles");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "afiliados");
        }
    }
}
