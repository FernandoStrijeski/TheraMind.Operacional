using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.ProfissionaisAcessos;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.ProfissionaisAcessos
{
    public class ProfissionalAcessoServico : ServicoBase, IProfissionalAcessoServico
    {
        private IConfiguration _configuration;
        private IProfissionalAcessoRepo _profissionalAcessoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ProfissionalAcessoServico(
            IConfiguration configuration,
            IProfissionalAcessoRepo profissionalAcessoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _profissionalAcessoRepo = profissionalAcessoRepo;
        }

        public async Task<ProfissionalAcesso>? BuscarPorID(int profissionalAcessoID) => await _profissionalAcessoRepo.BuscarPorID(profissionalAcessoID);

        public async Task<List<ProfissionalAcesso>> BuscarTodos()
        {
            return await _profissionalAcessoRepo.BuscarFiltros();
        }

        public async Task<List<ProfissionalAcesso>> BuscarPorIDProfissional(Guid profissionalID) => await _profissionalAcessoRepo.BuscarPorIDProfissional(profissionalID);


        public async Task<ProfissionalAcesso> Adicionar(ProfissionalAcesso profissionalAcesso)
        {
            await _profissionalAcessoRepo.Adicionar(profissionalAcesso);
            await Comitar();
            return profissionalAcesso;
        }

        public async Task<ProfissionalAcesso> Atualizar(ProfissionalAcesso profissionalAcesso)
        {
            await _profissionalAcessoRepo.Atualizar(profissionalAcesso);
            await Comitar();
            return profissionalAcesso;
        }

        public async Task Deletar(int profissionalAcessoID)
        {
            var profissionalAcesso = _profissionalAcessoRepo.BuscarPorID(profissionalAcessoID).Result;

            if (profissionalAcesso == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Acesso do profissional não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _profissionalAcessoRepo.Deletar(profissionalAcessoID);
            await Comitar();

            return;
        }
    }
}
