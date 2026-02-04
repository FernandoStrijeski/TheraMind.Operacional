using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelos;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Servicos.DocumentosModelos
{
    public class DocumentoModeloServico : ServicoBase, IDocumentoModeloServico
    {
        private IConfiguration _configuration;
        private IDocumentoModeloRepo _documentoModeloRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public DocumentoModeloServico(
            IConfiguration configuration,
            IDocumentoModeloRepo documentoModeloRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _documentoModeloRepo = documentoModeloRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<DocumentoModelo>? BuscarPorID(int documentoModeloID) => await _documentoModeloRepo.BuscarPorID(documentoModeloID);

        public async Task<List<DocumentoModelo>> BuscarTodos()
        {
            return await _documentoModeloRepo.BuscarFiltros();
        }

        public async Task<List<DocumentoModelo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _documentoModeloRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<DocumentoModelo> Adicionar(DocumentoModelo documentoModelo)
        {
            await _documentoModeloRepo.Adicionar(documentoModelo);
            await Comitar();
            return documentoModelo;
        }

        public async Task<DocumentoModelo> Atualizar(DocumentoModelo documentoModelo)
        {
            await _documentoModeloRepo.Atualizar(documentoModelo);
            await Comitar();
            return documentoModelo;
        }

        public async Task Deletar(int documentoModeloID)
        {
            var documentoModelo = _documentoModeloRepo.BuscarPorID(documentoModeloID).Result;

            if (documentoModelo == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Modelo de documento não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _documentoModeloRepo.Deletar(documentoModeloID);
            await Comitar();

            return;
        }
    }
}
