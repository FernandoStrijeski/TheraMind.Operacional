using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Filiais
{
    public class FilialServico : ServicoBase, IFilialServico
    {
        private IConfiguration _configuration;
        private IFilialRepo _filialRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public FilialServico(
            IConfiguration configuration,
            IFilialRepo filialRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _filialRepo = filialRepo;
        }

        public async Task<List<Dominio.Entidades.Filial>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _filialRepo.BuscarFiltros(x => x.NomeFilial.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<Dominio.Entidades.Filial>? BuscarPorID(int filialID) => await _filialRepo.BuscarPorID(filialID);

        public async Task<List<Dominio.Entidades.Filial>>? BuscarTodasPorEmpresa(Guid empresaID) => await _filialRepo.BuscarTodasPorEmpresa(empresaID);

        public async Task<Filial> Adicionar(Filial filial)
        {
            await _filialRepo.Adicionar(filial);
            await Comitar();
            return filial;
        }

        public async Task<Filial> Atualizar(Filial filial)
        {
            await _filialRepo.Atualizar(filial);
            await Comitar();
            return filial;
        }

        public async Task Deletar(int filialID)
        {
            var filial = _filialRepo.BuscarPorID(filialID).Result;

            if (filial == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Filial não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _filialRepo.Deletar(filialID);
            await Comitar();

            return;
        }
    }
}
