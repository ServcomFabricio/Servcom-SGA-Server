using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _05_12062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeStamp",
                table: "Atendimentos",
                nullable: false,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TimeStamp",
                table: "Atendimentos",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
