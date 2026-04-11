using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiapCloudGames.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InserirUsuarioSenhaCriptografada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "$2a$12$EDAGUpQWkCku4IoCMQBFg.9MeJtlL8iMdKyBQfkL0KpToNfaBkTAu", "A" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1,
                columns: new[] { "usuario_senha", "usuario_situacao" },
                values: new object[] { "admin", "\0" });
        }
    }
}
