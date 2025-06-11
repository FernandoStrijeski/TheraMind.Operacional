using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelosEmpresas;
using Dominio.DocumentosModelosEmpresasOpcoes;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.DocumentosModelosEmpresasOpcoes
{
    public class DocumentoModeloEmpresaOpcaoServico : ServicoBase, IDocumentoModeloEmpresaOpcaoServico
    {
        private IConfiguration _configuration;
        private IDocumentoModeloEmpresaOpcaoRepo _documentoModeloEmpresaOpcaoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public DocumentoModeloEmpresaOpcaoServico(
            IConfiguration configuration,
            IDocumentoModeloEmpresaOpcaoRepo documentoModeloEmpresaOpcaoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _documentoModeloEmpresaOpcaoRepo = documentoModeloEmpresaOpcaoRepo;
            _connectionParamsServico = connectionParamsServico;
        }

        public async Task<DocumentoModeloEmpresaOpcao>? BuscarPorID(int documentoModeloEmpresaID) => await _documentoModeloEmpresaOpcaoRepo.BuscarPorID(documentoModeloEmpresaID);

        public async Task<List<DocumentoModeloEmpresaOpcao>> BuscarTodos()
        {
            return await _documentoModeloEmpresaOpcaoRepo.BuscarFiltros();
        }

        public async Task<DocumentoModeloEmpresaOpcao> Adicionar(DocumentoModeloEmpresaOpcao documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaOpcaoRepo.Adicionar(documentoModeloEmpresa);
            await Comitar();
            return documentoModeloEmpresa;
        }

        public async Task<DocumentoModeloEmpresaOpcao> Atualizar(DocumentoModeloEmpresaOpcao documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaOpcaoRepo.Atualizar(documentoModeloEmpresa);
            await Comitar();
            return documentoModeloEmpresa;
        }

        public async Task Deletar(int documentoModeloEmpresaID)
        {
            var documentoModeloEmpresa = _documentoModeloEmpresaOpcaoRepo.BuscarPorID(documentoModeloEmpresaID).Result;

            if (documentoModeloEmpresa == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Opção do modelo do documento da empresa não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _documentoModeloEmpresaOpcaoRepo.Deletar(documentoModeloEmpresaID);
            await Comitar();

            return;
        }
    }
}
