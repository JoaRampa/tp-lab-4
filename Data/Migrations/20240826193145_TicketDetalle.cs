using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp_lab_4.Data.Migrations
{
    /// <inheritdoc />
    public partial class TicketDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalles_tickets_ticketsId",
                table: "TicketDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_afiliados_afiliadoId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "descripcion",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "estados",
                table: "TicketDetalles");

            migrationBuilder.RenameColumn(
                name: "fechaSolicitud",
                table: "tickets",
                newName: "FechaSolicitud");

            migrationBuilder.RenameColumn(
                name: "afiliadoId",
                table: "tickets",
                newName: "AfiliadoId");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_afiliadoId",
                table: "tickets",
                newName: "IX_tickets_AfiliadoId");

            migrationBuilder.RenameColumn(
                name: "descripcionPedido",
                table: "TicketDetalles",
                newName: "DescripcionPedido");

            migrationBuilder.RenameColumn(
                name: "ticketsId",
                table: "TicketDetalles",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalles_ticketsId",
                table: "TicketDetalles",
                newName: "IX_TicketDetalles_TicketId");

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EstadosId",
                table: "TicketDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetalles_EstadosId",
                table: "TicketDetalles",
                column: "EstadosId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalles_estados_EstadosId",
                table: "TicketDetalles",
                column: "EstadosId",
                principalTable: "estados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalles_tickets_TicketId",
                table: "TicketDetalles",
                column: "TicketId",
                principalTable: "tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_afiliados_AfiliadoId",
                table: "tickets",
                column: "AfiliadoId",
                principalTable: "afiliados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalles_estados_EstadosId",
                table: "TicketDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketDetalles_tickets_TicketId",
                table: "TicketDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_tickets_afiliados_AfiliadoId",
                table: "tickets");

            migrationBuilder.DropIndex(
                name: "IX_TicketDetalles_EstadosId",
                table: "TicketDetalles");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "EstadosId",
                table: "TicketDetalles");

            migrationBuilder.RenameColumn(
                name: "FechaSolicitud",
                table: "tickets",
                newName: "fechaSolicitud");

            migrationBuilder.RenameColumn(
                name: "AfiliadoId",
                table: "tickets",
                newName: "afiliadoId");

            migrationBuilder.RenameIndex(
                name: "IX_tickets_AfiliadoId",
                table: "tickets",
                newName: "IX_tickets_afiliadoId");

            migrationBuilder.RenameColumn(
                name: "DescripcionPedido",
                table: "TicketDetalles",
                newName: "descripcionPedido");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketDetalles",
                newName: "ticketsId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketDetalles_TicketId",
                table: "TicketDetalles",
                newName: "IX_TicketDetalles_ticketsId");

            migrationBuilder.AddColumn<bool>(
                name: "descripcion",
                table: "tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "estados",
                table: "TicketDetalles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketDetalles_tickets_ticketsId",
                table: "TicketDetalles",
                column: "ticketsId",
                principalTable: "tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tickets_afiliados_afiliadoId",
                table: "tickets",
                column: "afiliadoId",
                principalTable: "afiliados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
