using API.Core.Exceptions;
using API.Servicos;
using Dominio.Entidades;
using Dominio.Repositorios;
using Moq;
using Xunit;

namespace tests.API.tests.Services
{
    public class EmpresaServicoTests
    {
        private Mock<IEmpresaRepo> repo;

        public EmpresaServicoTests()
        {
            repo = new Mock<IEmpresaRepo>();
        }

        [Fact]
        public async void ValidaEmpresa_EmpresaNaoEncontrada_LancaHttpErroDeUsuario()
        {
            // Arrange
            repo.Setup(x => x.Buscar(y => y.EmpCodEmpresa == Faker.Name.First(), null, "", 0, 0))
                .Returns(Task.FromResult(null as IEnumerable<Empresa?>));

            var servico = new EmpresaServico(repo.Object);
            var codEmpresa = "123";
            // Act
            // Assert
            await Assert.ThrowsAsync<HttpErroDeUsuario>(() => servico.ValidaEmpresa(codEmpresa));
            repo.Verify(x => x.Buscar(y => y.EmpCodEmpresa == codEmpresa, null, "", 0, 0), Times.Once);
        }

        [Fact]
        public async void ValidaEmpresa_EmpresaEncontrada_RetornaVazio()
        {
            // Arrange
            var codEmpresa = Faker.Name.First();
            IEnumerable<Empresa> empresa = new List<Empresa>() { new Empresa() };
            repo.Setup(x => x.Buscar(y => y.EmpCodEmpresa == codEmpresa, null, "", 0, 0))
                .Returns(Task.FromResult(empresa));
            var servico = new EmpresaServico(repo.Object);
            // Act
            try
            {
                await servico.ValidaEmpresa(codEmpresa);
                repo.Verify(x => x.Buscar(y => y.EmpCodEmpresa == codEmpresa, null, "", 0, 0), Times.Once);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.Fail("Esperava nenhum erro, mas ocorreu: " + ex.Message);
            }
        }
    }
}
