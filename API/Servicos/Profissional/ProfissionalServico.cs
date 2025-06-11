using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Profissionais
{
    public class ProfissionalServico : ServicoBase, IProfissionalServico
    {
        private IConfiguration _configuration;
        private IProfissionalRepo _profissionalRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ProfissionalServico(
            IConfiguration configuration,
            IProfissionalRepo profissionalRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _profissionalRepo = profissionalRepo;
        }

        public async Task<Profissional>? BuscarPorID(Guid profissionalID) => await _profissionalRepo.BuscarPorID(profissionalID);

        public async Task<List<Profissional>> BuscarTodos()
        {
            return await _profissionalRepo.BuscarFiltros();
        }

        public async Task<List<Profissional>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _profissionalRepo.BuscarFiltros(x => x.NomeCompleto.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Profissional> Adicionar(Profissional profissional)
        {
            await _profissionalRepo.Adicionar(profissional);
            await Comitar();
            return profissional;
        }

        public async Task<Profissional> Atualizar(Profissional profissional)
        {
            await _profissionalRepo.Atualizar(profissional);
            await Comitar();
            return profissional;
        }

        public async Task Deletar(Guid profissionalID)
        {
            var profissional = _profissionalRepo.BuscarPorID(profissionalID).Result;

            if (profissional == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Profissional não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _profissionalRepo.Deletar(profissionalID);
            await Comitar();

            return;
        }
    }
}
