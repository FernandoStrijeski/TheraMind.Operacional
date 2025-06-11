using API.Core.Exceptions;
using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.EstadosCivis
{
    public class EstadoCivilServico : ServicoBase, IEstadoCivilServico
    {
        private IConfiguration _configuration;
        private IEstadoCivilRepo _estadoCivilRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public EstadoCivilServico(
            IConfiguration configuration,
            IEstadoCivilRepo estadoCivilRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _estadoCivilRepo = estadoCivilRepo;
        }

        public async Task<EstadoCivil>? BuscarPorID(string estadoCivilID) => await _estadoCivilRepo.BuscarPorID(estadoCivilID);

        public async Task<List<EstadoCivil>> BuscarTodos()
        {
            return await _estadoCivilRepo.BuscarFiltros();
        }

        public async Task<List<EstadoCivil>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _estadoCivilRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<EstadoCivil> Adicionar(EstadoCivil estadoCivil)
        {
            await _estadoCivilRepo.Adicionar(estadoCivil);
            await Comitar();
            return estadoCivil;
        }

        public async Task<EstadoCivil> Atualizar(EstadoCivil estadoCivil)
        {
            await _estadoCivilRepo.Atualizar(estadoCivil);
            await Comitar();
            return estadoCivil;
        }

        public async Task Deletar(string estadoCivilID)
        {
            var estadoCivil = _estadoCivilRepo.BuscarPorID(estadoCivilID).Result;

            if (estadoCivil == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Estado civil não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _estadoCivilRepo.Deletar(estadoCivilID);
            await Comitar();

            return;
        }
    }
}
