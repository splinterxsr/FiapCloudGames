using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterarTamanhoNomeJogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "jogo_nome",
                table: "tbl_jogo",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "$2a$12$svXQ729NloenitUUO6SVVuQ/dozPgz43tm/rOrMIDppxix0YFtYJW", "A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "jogo_nome",
                table: "tbl_jogo",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "$2a$12$EDAGUpQWkCku4IoCMQBFg.9MeJtlL8iMdKyBQfkL0KpToNfaBkTAu", "\0" });
        }
    }
}
