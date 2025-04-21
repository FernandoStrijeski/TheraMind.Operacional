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
    public class CesCarcoServicoTests
    {
        private Mock<IConfiguration> config;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<ICesCargoRepo> repo;

        public CesCarcoServicoTests()
        {
            config = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            repo = new Mock<ICesCargoRepo>();
        }

        [Fact]
        public async void CriarParaImportacao_Cria_Se_N_Existir()
        {
            // Arrange
            var codEmpresa = "123";
            var codCargo = "456";
            IEnumerable<CesCargo?> cargo = new List<CesCargo?>() { (CesCargo)null }.AsEnumerable();
            repo.Setup(x => x.Buscar(y => y.CodCargo == codCargo, null, "", 0, 0))
                .ReturnsAsync(cargo);
            var servico = new CesCargoServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                repo.Object
            );
            // Act
            await servico.CriarParaImportacao(codEmpresa, codCargo);
            // Assert
            repo.Verify(x => x.Buscar(y => y.CodCargo == codCargo, null, "", 0, 0), Times.Once);
            repo.Verify(x => x.Adicionar(It.IsAny<CesCargo>()), Times.Once);
            repo.Verify(x => x.Atualizar(It.IsAny<CesCargo>()), Times.Never);
        }

        [Fact]
        public async void CriarParaImportacao_N_Cria_Se_Existir()
        {
            // Arrange
            var codEmpresa = "123";
            var codCargo = "456";
            var cargo = (new List<CesCargo> { new CesCargo() }).AsEnumerable();

            repo.Setup(x => x.Buscar(y => y.CodCargo == codCargo, null, "", 0, 0))
                .Returns(Task.FromResult(cargo));
            var servico = new CesCargoServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                repo.Object
            );
            // Act
            await servico.CriarParaImportacao(codEmpresa, codCargo);
            // Assert

            repo.Verify(x => x.Buscar(y => y.CodCargo == codCargo, null, "", 0, 0), Times.Once);

            repo.Verify(x => x.Adicionar(It.IsAny<CesCargo>()), Times.Never);
            repo.Verify(x => x.Atualizar(It.IsAny<CesCargo>()), Times.Never);
        }
    }
}
