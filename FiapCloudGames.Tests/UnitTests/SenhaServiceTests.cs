using FiapCloudGames.Infra.Data.Services;

namespace FiapCloudGames.Tests.UnitTests
{
    public class SenhaServiceTests
    {
        [Fact(DisplayName = "CriaHash não deve retornar o mesmo hash para senhas diferentes")]
        [Trait("Categoria", "Unitário")]
        public void CriaHash_NaoDeveRetornarMesmoHashParaMesmaSenha()
        {
            // Arrange
            var senhaService = new SenhaService();
            var senha = "minhaSenhaSegura";
            // Act
            var hash1 = senhaService.CriaHash(senha);
            var hash2 = senhaService.CriaHash(senha);
            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact(DisplayName = "ValidaSenha deve retornar true para senha válida")]
        [Trait("Categoria", "Unitário")]
        public void ValidaSenha_DeveRetornarTrueParaSenhaValida()
        {
            // Arrange
            var senhaService = new SenhaService();
            var senha = "minhaSenhaSegura";
            var hash = senhaService.CriaHash(senha);
            // Act
            var resultado = senhaService.ValidaSenha(senha, hash);
            // Assert
            Assert.True(resultado);
        }
    }
}