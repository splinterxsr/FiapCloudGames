using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_jogo",
                columns: table => new
                {
                    jogo_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    jogo_nome = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jogo_situacao = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "A")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jogo_datahora = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_jogo", x => x.jogo_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_perfil",
                columns: table => new
                {
                    perfil_id = table.Column<short>(type: "smallint(2)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    perfil_nome = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    perfil_datahora = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_perfil", x => x.perfil_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tbl_usuario",
                columns: table => new
                {
                    usuario_id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    usuario_email = table.Column<string>(type: "varchar(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_senha = table.Column<string>(type: "varchar(15)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    perfil_id = table.Column<short>(type: "smallint(2)", nullable: false),
                    usuario_nome = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_situacao = table.Column<string>(type: "char(1)", nullable: false, defaultValue: "A")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_datahora = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "current_timestamp()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usuario", x => x.usuario_id);
                    table.ForeignKey(
                        name: "FK_tbl_usuario_tbl_perfil_perfil_id",
                        column: x => x.perfil_id,
                        principalTable: "tbl_perfil",
                        principalColumn: "perfil_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuario_perfil_id",
                table: "tbl_usuario",
                column: "perfil_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_jogo");

            migrationBuilder.DropTable(
                name: "tbl_usuario");

            migrationBuilder.DropTable(
                name: "tbl_perfil");
        }
    }
}
