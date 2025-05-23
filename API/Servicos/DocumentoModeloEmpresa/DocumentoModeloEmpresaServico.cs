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

        public async Task Salvar(DocumentoModeloEmpresa documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaRepo.Adicionar(documentoModeloEmpresa);
            await Comitar();
        }

        private async Task Atualizar(DocumentoModeloEmpresa documentoModeloEmpresa)
        {
            await _documentoModeloEmpresaRepo.Atualizar(documentoModeloEmpresa);
            await Comitar();
        }

        public async Task<(bool criado, int documentoModeloEmpresaId)> CriarOuAtualizar(CriarDocumentoModeloEmpresaInputModel documentoModeloEmpresa, bool atualizaSeExistir)
        {
            var cDocumentoModeloEmpresa = (await _documentoModeloEmpresaRepo.Buscar(
                x => x.DocumentoModeloEmpresaID == documentoModeloEmpresa.DocumentoModeloEmpresaID
            )).FirstOrDefault();

            if (cDocumentoModeloEmpresa == null)
            {
                cDocumentoModeloEmpresa = DocumentoModeloEmpresa.CriarParaImportacao(
                    empresaId: documentoModeloEmpresa.EmpresaId,
                    filialId: documentoModeloEmpresa.FilialId,
                    tipoDocumentoId: documentoModeloEmpresa.TipoDocumentoId,
                    titulo: documentoModeloEmpresa.Titulo,
                    conteudoTipo: documentoModeloEmpresa.ConteudoTipo,
                    conteudo: documentoModeloEmpresa.Conteudo,
                    ativo: documentoModeloEmpresa.Ativo
                );
                await Salvar(cDocumentoModeloEmpresa);
                return (true, cDocumentoModeloEmpresa.DocumentoModeloEmpresaID); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cDocumentoModeloEmpresa.AtualizarPropriedades(
                    empresaId: documentoModeloEmpresa.EmpresaId,
                    filialId: documentoModeloEmpresa.FilialId,
                    tipoDocumentoId: documentoModeloEmpresa.TipoDocumentoId,
                    titulo: documentoModeloEmpresa.Titulo,
                    conteudoTipo: documentoModeloEmpresa.ConteudoTipo,
                    conteudo: documentoModeloEmpresa.Conteudo,
                    ativo: documentoModeloEmpresa.Ativo
                );
                await _documentoModeloEmpresaRepo.Atualizar(cDocumentoModeloEmpresa);
                await Atualizar(cDocumentoModeloEmpresa);
            }

            return (false, documentoModeloEmpresa.DocumentoModeloEmpresaID);
        }


        public async Task CriarParaImportacao(int documentoModeloEmpresaID, Guid empresaID, int filialID, int tipoDocumentoId, string titulo, short conteudoTipo, string conteudo, bool? ativo)
        {
            var cDocumentoModeloEmpresa = (await _documentoModeloEmpresaRepo.Buscar(
                            x => x.DocumentoModeloEmpresaID == documentoModeloEmpresaID)
                            ).FirstOrDefault();
            if (cDocumentoModeloEmpresa == null)
            {
                cDocumentoModeloEmpresa = DocumentoModeloEmpresa.CriarParaImportacao(empresaID, filialID, tipoDocumentoId, titulo, conteudoTipo, conteudo, ativo);
                await Salvar(cDocumentoModeloEmpresa);
            }
            return;
        }

        public async Task Validar(int documentoModeloEmpresaID)
        {
            var cDocumentoModeloEmpresa = (await _documentoModeloEmpresaRepo.Buscar(x => x.DocumentoModeloEmpresaID == documentoModeloEmpresaID)).FirstOrDefault();
            if (cDocumentoModeloEmpresa == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Modelo de documento da empresa com ID {documentoModeloEmpresaID} não encontrado."
                );
            }
        }
    }
}
