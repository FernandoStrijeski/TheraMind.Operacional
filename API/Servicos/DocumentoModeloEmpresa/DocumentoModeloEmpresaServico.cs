using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelosEmpresas;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.DocumentosModelosEmpresas
{
    public class DocumentoModeloEmpresaServico : ServicoBase, IDocumentoModeloEmpresaServico
    {
        private IConfiguration _configuration;
        private IDocumentoModeloEmpresaRepo _documentoModeloEmpresaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public DocumentoModeloEmpresaServico(
            IConfiguration configuration,
            IDocumentoModeloEmpresaRepo documentoModeloEmpresaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _documentoModeloEmpresaRepo = documentoModeloEmpresaRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<DocumentoModeloEmpresa>? BuscarPorID(int documentoModeloEmpresaID) => await _documentoModeloEmpresaRepo.BuscarPorID(documentoModeloEmpresaID);

        public async Task<List<DocumentoModeloEmpresa>> BuscarTodos()
        {
            return await _documentoModeloEmpresaRepo.BuscarFiltros();
        }

        public async Task<List<DocumentoModeloEmpresa>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _documentoModeloEmpresaRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task<DocumentoModeloEmpresa> Adicionar(DocumentoModeloEmpresa documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaRepo.Adicionar(documentoModeloEmpresa);
            await Comitar();
            return documentoModeloEmpresa;
        }

        public async Task<DocumentoModeloEmpresa> Atualizar(DocumentoModeloEmpresa documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaRepo.Atualizar(documentoModeloEmpresa);
            await Comitar();
            return documentoModeloEmpresa;
        }

        public async Task Deletar(int documentoModeloEmpresaID)
        {
            var documentoModeloEmpresa = _documentoModeloEmpresaRepo.BuscarPorID(documentoModeloEmpresaID).Result;

            if (documentoModeloEmpresa == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Modelo de documento da empresa não encontrado, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _documentoModeloEmpresaRepo.Deletar(documentoModeloEmpresaID);
            await Comitar();

            return;
        }
    }
}
