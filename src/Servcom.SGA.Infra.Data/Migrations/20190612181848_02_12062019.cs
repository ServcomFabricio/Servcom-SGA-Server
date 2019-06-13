using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _02_12062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                table: "Atendimentos");

            migrationBuilder.DropIndex(
                name: "IX_Atendimentos_TipoId",
                table: "Atendimentos");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoId",
                table: "Atendimentos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Atendimentos_TipoId_DataCriacao_Sequencia",
                table: "Atendimentos",
                columns: new[] { "TipoId", "DataCriacao", "Sequencia" });

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                table: "Atendimentos",
                column: "TipoId",
                principalTable: "TipoAtendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                table: "Atendimentos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Atendimentos_TipoId_DataCriacao_Sequencia",
                table: "Atendimentos");

            migrationBuilder.AlterColumn<Guid>(
                name: "TipoId",
                table: "Atendimentos",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_TipoId",
                table: "Atendimentos",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                table: "Atendimentos",
                column: "TipoId",
                principalTable: "TipoAtendimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
