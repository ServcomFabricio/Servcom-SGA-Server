using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _01_14062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraChamada",
                table: "Atendimentos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Guiche",
                table: "Atendimentos",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Prioritario",
                table: "Atendimentos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraChamada",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "Guiche",
                table: "Atendimentos");

            migrationBuilder.DropColumn(
                name: "Prioritario",
                table: "Atendimentos");
        }
    }
}
