using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.TiposDocumentos
{
    public class TipoDocumentoServico : ServicoBase, ITipoDocumentoServico
    {
        private IConfiguration _configuration;
        private ITipoDocumentoRepo _tipoDocumentoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public TipoDocumentoServico(
            IConfiguration configuration,
            ITipoDocumentoRepo tipoDocumentoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _tipoDocumentoRepo = tipoDocumentoRepo;
        }
        
        public async Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID) => await _tipoDocumentoRepo.BuscarPorID(tipoDocumentoID);

        public async Task<List<TipoDocumento>> BuscarTodos()
        {
            return await _tipoDocumentoRepo.BuscarFiltros();
        }

        public async Task<List<TipoDocumento>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _tipoDocumentoRepo.BuscarFiltros(x => x.Descricao.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<TipoDocumento> Adicionar(TipoDocumento tipoDocumento)
        {
            await _tipoDocumentoRepo.Adicionar(tipoDocumento);
            await Comitar();
            return tipoDocumento;
        }

        public async Task<TipoDocumento> Atualizar(TipoDocumento tipoDocumento)
        {
            await _tipoDocumentoRepo.Atualizar(tipoDocumento);
            await Comitar();
            return tipoDocumento;
        }

        public async Task Deletar(int tipoDocumentoID)
        {
            var tipoDocumento = _tipoDocumentoRepo.BuscarPorID(tipoDocumentoID).Result;

            if (tipoDocumento == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Tipo de documento não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _tipoDocumentoRepo.Deletar(tipoDocumentoID);
            await Comitar();

            return;
        }
    }
}
