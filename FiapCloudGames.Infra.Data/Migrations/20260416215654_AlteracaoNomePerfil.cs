using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoNomePerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tbl_perfil",
                keyColumn: "perfil_id",
                keyValue: (short)2,
                column: "perfil_nome",
                value: "Usuario");

            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "$2a$12$KggauqETCQygOgUhKASVIejArq6j9aOpwiS9vER7m5fjDKhXy6vsW", "A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tbl_perfil",
                keyColumn: "perfil_id",
                keyValue: (short)2,
                column: "perfil_nome",
                value: "Usuário");

            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "$2a$12$svXQ729NloenitUUO6SVVuQ/dozPgz43tm/rOrMIDppxix0YFtYJW", "\0" });
        }
    }
}
