using API.Core.Exceptions;
using API.Servicos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace tests.API.tests.Services
{
    public class SituacaoServicoTests
    {
        private Mock<ISituacaoRepo> repo;
        private Mock<IConfiguration> config;
        private Mock<IUnitOfWork> unitOfWork;

        public SituacaoServicoTests()
        {
            repo = new Mock<ISituacaoRepo>();
            config = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void ValidaSituacao_SituacaoNaoEncontrada_LancaHttpErroDeUsuario()
        {
            // Arrange
            short codSituacao = 123;
            string sCodSituacao = "123";
            IEnumerable<Situacao> resultadoQueryVazio = Enumerable.Empty<Situacao>();
            repo.Setup(x => x.Buscar(x => x.CadReserva == sCodSituacao, null, "", 0, 0))
                .Returns(Task.FromResult(resultadoQueryVazio));

            var servico = new SituacaoServico(config.Object, unitOfWork.Object, repo.Object);
            // Act

            // Assert
            Assert.ThrowsAsync<HttpErroDeUsuario>(() => servico.ValidaSituacao(codSituacao));
        }

        [Fact]
        public async void ValidaSituacao_SituacaoEncontrada_RetornaVazio()
        {
            // Arrange
            short codSituacao = 123;
            string sCodSituacao = "123";

            var sit = Situacao.Criar(sCodSituacao);
            IEnumerable<Situacao> resultadoQuery = new List<Situacao>() { sit };
            repo.Setup(x => x.Buscar(x => x.CadReserva == sCodSituacao, null, "", 0, 0))
                .Returns(Task.FromResult(resultadoQuery));
            var servico = new SituacaoServico(config.Object, unitOfWork.Object, repo.Object);
            // Act
            try
            {
                await servico.ValidaSituacao(codSituacao);
            }
            catch (HttpErroDeUsuario ex)
            {
                // Assert
                Assert.Fail("Esperava nenhum erro, mas ocorreu: " + ex.Message);
            }
        }
    }
}
