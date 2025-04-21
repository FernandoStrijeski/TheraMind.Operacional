using API.Core.Exceptions;
using API.Servicos;
using AutoMapper;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace tests.API.tests.Services
{
    public class SindicatoServicoTests
    {
        private Mock<ISindicatoRepo> repo;
        private Mock<IConfiguration> config;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;

        public SindicatoServicoTests()
        {
            repo = new Mock<ISindicatoRepo>();
            config = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async void CriarParaImportacao_CriaNovoSindicato()
        {
            // Setup
            string sCodSindicato = "123";
            repo.Setup((x => x.Buscar(y => y.SinCodSindicato == sCodSindicato, null, "", 0, 0)))
                .Returns(Task.FromResult(new List<Sindicato>() as IEnumerable<Sindicato?>));
            var servico = new SindicatoServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                repo.Object
            );
            // Act

            await servico.CriarParaImportacao(sCodSindicato);


            repo.Verify(x => x.Adicionar(It.IsAny<Sindicato>()), Times.Once);
            repo.Verify(x => x.Buscar(y => y.SinCodSindicato == sCodSindicato, null, "", 0, 0), Times.Once);
            repo.Verify(x => x.Atualizar(It.IsAny<Sindicato>()), Times.Never);
        }

        [Fact]
        public async void CriarParaImportacao_Nao_Cria_Novo_Sindicato_Se_Existir()
        {
            // Setup
            string sCodSindicato = "123";
            repo.Setup(x => x.Buscar(y => y.SinCodSindicato == sCodSindicato, null, "", 0, 0))
                .Returns(Task.FromResult(new List<Sindicato>() { new Sindicato() } as IEnumerable<Sindicato?>));
            var servico = new SindicatoServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                repo.Object
            );
            // Act
            try
            {
                await servico.CriarParaImportacao(sCodSindicato);
            }
            catch (Exception ex)
            {
                Assert.Fail(
                    "Esperava não lançar nenhum erro, mas ocorreu com a mensagem: " + ex.Message
                );
            }
            // Assert
            repo.Verify(x => x.Adicionar(It.IsAny<Sindicato>()), Times.Never);
            repo.Verify(x => x.Buscar(y => y.SinCodSindicato == sCodSindicato, null, "", 0, 0), Times.Once);
            repo.Verify(x => x.Atualizar(It.IsAny<Sindicato>()), Times.Never);
        }
    }
}
