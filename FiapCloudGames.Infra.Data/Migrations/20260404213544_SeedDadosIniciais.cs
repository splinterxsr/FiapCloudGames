using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FiapCloudGames.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDadosIniciais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_perfil",
                columns: new[] { "perfil_id", "perfil_nome" },
                values: new object[,]
                {
                    { (short)1, "Administrador" },
                    { (short)2, "Usuário" }
                });

            migrationBuilder.InsertData(
                table: "tbl_usuario",
                columns: new[] { "usuario_id", "usuario_email", "usuario_nome", "perfil_id", "usuario_senha" },
                values: new object[] { 1, "admin@fiapcloud.com.br", "Admin", (short)1, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_perfil",
                keyColumn: "perfil_id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "tbl_usuario",
                keyColumn: "usuario_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbl_perfil",
                keyColumn: "perfil_id",
                keyValue: (short)1);
        }
    }
}
