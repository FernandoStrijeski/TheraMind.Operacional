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

        public async Task Salvar(DocumentoModeloEmpresaOpcao documentoModeloEmpresaOpcao)
        {
            await _documentoModeloEmpresaOpcaoRepo.Adicionar(documentoModeloEmpresaOpcao);
            await Comitar();
        }

        private async Task Atualizar(DocumentoModeloEmpresaOpcao documentoModeloEmpresaOpcao)
        {
            await _documentoModeloEmpresaOpcaoRepo.Atualizar(documentoModeloEmpresaOpcao);
            await Comitar();
        }

        public async Task<(bool criado, int documentoModeloEmpresaOpcaoId)> CriarOuAtualizar(CriarDocumentoModeloEmpresaOpcaoInputModel documentoModeloEmpresaOpcao, bool atualizaSeExistir)
        {
            var cDocumentoModeloEmpresaOpcao = (await _documentoModeloEmpresaOpcaoRepo.Buscar(
                x => x.DocumentoModeloEmpresaOpcaoId == documentoModeloEmpresaOpcao.DocumentoModeloEmpresaOpcaoID
            )).FirstOrDefault();

            if (cDocumentoModeloEmpresaOpcao == null)
            {
                cDocumentoModeloEmpresaOpcao = DocumentoModeloEmpresaOpcao.CriarParaImportacao(
                    empresaId: documentoModeloEmpresaOpcao.EmpresaId,
                    filialId: documentoModeloEmpresaOpcao.FilialId,
                    tipoOpcao: documentoModeloEmpresaOpcao.TipoOpcao,                    
                    conteudoBase64: documentoModeloEmpresaOpcao.ConteudoBase64,
                    transparencia: documentoModeloEmpresaOpcao.Transparencia,
                    ativo: documentoModeloEmpresaOpcao.Ativo
                );
                await Salvar(cDocumentoModeloEmpresaOpcao);
                return (true, cDocumentoModeloEmpresaOpcao.DocumentoModeloEmpresaOpcaoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cDocumentoModeloEmpresaOpcao.AtualizarPropriedades(
                    empresaId: documentoModeloEmpresaOpcao.EmpresaId,
                    filialId: documentoModeloEmpresaOpcao.FilialId,
                    tipoOpcao: documentoModeloEmpresaOpcao.TipoOpcao,                    
                    conteudoBase64: documentoModeloEmpresaOpcao.ConteudoBase64,
                    transparencia: documentoModeloEmpresaOpcao.Transparencia,
                    ativo: documentoModeloEmpresaOpcao.Ativo
                );
                await _documentoModeloEmpresaOpcaoRepo.Atualizar(cDocumentoModeloEmpresaOpcao);
                await Atualizar(cDocumentoModeloEmpresaOpcao);
            }

            return (false, documentoModeloEmpresaOpcao.DocumentoModeloEmpresaOpcaoID);
        }


        public async Task CriarParaImportacao(int documentoModeloEmpresaOpcaoID, Guid empresaID, int filialID, short tipoOpcao, string conteudoBase64, decimal? transparencia, bool? ativo)
        {
            var cDocumentoModeloEmpresa = (await _documentoModeloEmpresaOpcaoRepo.Buscar(
                            x => x.DocumentoModeloEmpresaOpcaoId == documentoModeloEmpresaOpcaoID)
                            ).FirstOrDefault();
            if (cDocumentoModeloEmpresa == null)
            {
                cDocumentoModeloEmpresa = DocumentoModeloEmpresaOpcao.CriarParaImportacao(empresaID, filialID, tipoOpcao, conteudoBase64, transparencia, ativo);
                await Salvar(cDocumentoModeloEmpresa);
            }
            return;
        }

        public async Task Validar(int documentoModeloEmpresaOpcaoID)
        {
            var cDocumentoModeloEmpresa = (await _documentoModeloEmpresaOpcaoRepo.Buscar(x => x.DocumentoModeloEmpresaOpcaoId == documentoModeloEmpresaOpcaoID)).FirstOrDefault();
            if (cDocumentoModeloEmpresa == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Opção para o modelo de documento da empresa com ID {documentoModeloEmpresaOpcaoID} não encontrado."
                );
            }
        }
    }
}
