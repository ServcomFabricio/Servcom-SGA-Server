using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _04_12062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TimeStamp",
                table: "Atendimentos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Atendimentos");
        }
    }
}
