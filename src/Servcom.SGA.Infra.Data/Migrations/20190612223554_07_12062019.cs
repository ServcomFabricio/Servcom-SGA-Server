using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _07_12062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Atendimentos",
                type: "CHAR(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoId",
                table: "Atendimentos",
                type: "CHAR(36)",
                nullable: false,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Atendimentos",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "CHAR(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoId",
                table: "Atendimentos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "CHAR(36)");
        }
    }
}
