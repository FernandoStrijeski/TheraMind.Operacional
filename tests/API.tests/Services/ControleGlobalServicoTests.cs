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
    public class ControleGlobalServicoTestes
    {
        private Mock<IConfiguration> config;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<IControleGlobalRepo> controleGlobalRepo;

        public ControleGlobalServicoTestes()
        {
            config = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            controleGlobalRepo = new Mock<IControleGlobalRepo>();
        }

        [Fact]
        public async void CriaNovoRegistroQuantoNaoExistir_UpsertControleGlobal()
        {
            // Setup
            IQueryable<ControleGlobal> resultadoQueryVazio = Enumerable
                .Empty<ControleGlobal>()
                .AsQueryable();
            controleGlobalRepo
                .Setup(x => x.BuscarTodos())
                .Returns(Task.FromResult(resultadoQueryVazio));

            var servico = new ControleGlobalServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                controleGlobalRepo.Object
            );

            // Act
            var resultado = await servico.UpsertControleGlobal();

            // Assert
            Assert.True(resultado == 1);
            controleGlobalRepo.Verify(x => x.Adicionar(It.IsAny<ControleGlobal>()), Times.Once);
            controleGlobalRepo.Verify(
                x => x.DeletarControleGlobal(It.IsAny<ControleGlobal>()),
                Times.Never
            );
            controleGlobalRepo.Verify(x => x.BuscarTodos(), Times.Once);
        }

        [Fact]
        public async void CriaNovoRegistroQuantoExistir_UpsertControleGlobal()
        {
            // Setup
            var controleGlobal = new ControleGlobal();
            controleGlobal = controleGlobal.CloneSuperior(); // Retorna um controle global com valor 1
            IQueryable<ControleGlobal> resultadoQuery = new List<ControleGlobal>
            {
                controleGlobal
            }.AsQueryable();
            controleGlobalRepo.Setup(x => x.BuscarTodos()).Returns(Task.FromResult(resultadoQuery));
            var servico = new ControleGlobalServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                controleGlobalRepo.Object
            );
            // Act
            var resultado = await servico.UpsertControleGlobal();
            // Assert
            Assert.True(resultado == 2);
            controleGlobalRepo.Verify(x => x.Adicionar(It.IsAny<ControleGlobal>()), Times.Once);
            controleGlobalRepo.Verify(
                x => x.DeletarControleGlobal(It.IsAny<ControleGlobal>()),
                Times.Once
            );
            controleGlobalRepo.Verify(x => x.BuscarTodos(), Times.Once);
        }
    }
}
