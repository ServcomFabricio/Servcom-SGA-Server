using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _01_07062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoAtendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    Prioritario = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAtendimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Sigla = table.Column<string>(type: "varchar(3)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Setor = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: false),
                    HoraCriacao = table.Column<DateTime>(nullable: false),
                    DataHoraInicio = table.Column<DateTime>(nullable: false),
                    DataHoraFim = table.Column<DateTime>(nullable: false),
                    DataHoraultimoReingresso = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: true),
                    TipoId = table.Column<Guid>(nullable: true),
                    Usuario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoAtendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_TipoId",
                table: "Atendimentos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_UsuarioId",
                table: "Atendimentos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "TipoAtendimentos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
