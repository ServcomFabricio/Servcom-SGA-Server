using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servcom.SGA.Infra.Data.Migrations
{
    public partial class _01_02072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoConteudo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Conteudo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoConteudo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracaoGeral",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TituloPainelAtendimento = table.Column<string>(nullable: true),
                    TextoFixoPainelAtendimento = table.Column<string>(nullable: true),
                    EntradaVideo = table.Column<bool>(nullable: false),
                    ConteudoConfigurado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoGeral", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoAtendimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Prioritario = table.Column<bool>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
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
                    Sequencia = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: false),
                    HoraCriacao = table.Column<string>(nullable: true),
                    DataHoraInicio = table.Column<DateTime>(nullable: false),
                    DataHoraFim = table.Column<DateTime>(nullable: false),
                    DataHoraultimoReingresso = table.Column<DateTime>(nullable: false),
                    DataHoraChamada = table.Column<DateTime>(nullable: false),
                    Guiche = table.Column<string>(nullable: true),
                    Prioritario = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<Guid>(type: "CHAR(36)", nullable: true),
                    TipoId = table.Column<Guid>(type: "CHAR(36)", nullable: false),
                    Senha = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.UniqueConstraint("AK_Atendimentos_TipoId_DataCriacao_Sequencia", x => new { x.TipoId, x.DataCriacao, x.Sequencia });
                    table.ForeignKey(
                        name: "FK_Atendimentos_TipoAtendimentos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoAtendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "ConfiguracaoConteudo");

            migrationBuilder.DropTable(
                name: "ConfiguracaoGeral");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "TipoAtendimentos");
        }
    }
}
