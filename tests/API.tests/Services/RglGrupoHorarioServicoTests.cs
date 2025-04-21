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
    public class RglGrupoHorarioServicoTests
    {
        private Mock<IConfiguration> config;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IMapper> mapper;
        private Mock<IRglGrupoHorarioRepo> rglGrupoHorarioRepo;
        private RglGrupoHorarioServico servico;

        public RglGrupoHorarioServicoTests()
        {
            config = new Mock<IConfiguration>();
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            rglGrupoHorarioRepo = new Mock<IRglGrupoHorarioRepo>();
            servico = new RglGrupoHorarioServico(
                config.Object,
                unitOfWork.Object,
                mapper.Object,
                rglGrupoHorarioRepo.Object
            );
        }

        [Fact]
        public async void CriarParaImportacao_Cria_Se_N_Exise()
        {
            var codEmpresa = "123";
            await servico.CriarParaImportacao(codEmpresa);
            rglGrupoHorarioRepo.Verify(
                x => x.BuscarPorCodEmpresaECodGrupo(codEmpresa, "G001"),
                Times.Once
            );
            rglGrupoHorarioRepo.Verify(x => x.Adicionar(It.IsAny<RglGrupoHorario>()), Times.Once);
        }

        [Fact]
        public async void CriarParaImportacao_Nao_Cria_Se_Existe()
        {
            var codEmpresa = "123";
            rglGrupoHorarioRepo
                .Setup(x => x.BuscarPorCodEmpresaECodGrupo(codEmpresa, "G001"))
                .ReturnsAsync(new RglGrupoHorario());
            await servico.CriarParaImportacao(codEmpresa);
            rglGrupoHorarioRepo.Verify(
                x => x.BuscarPorCodEmpresaECodGrupo(codEmpresa, "G001"),
                Times.Once
            );
            rglGrupoHorarioRepo.Verify(x => x.Adicionar(It.IsAny<RglGrupoHorario>()), Times.Never);
        }
    }
}
