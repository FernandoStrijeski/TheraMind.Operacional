using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.DocumentosModelos;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(DocumentoModelo documentoModelo)
        {
            await _documentoModeloRepo.Adicionar(documentoModelo);
            await Comitar();
        }

        private async Task Atualizar(DocumentoModelo documentoModelo)
        {
            await _documentoModeloRepo.Atualizar(documentoModelo);
            await Comitar();
        }

        public async Task<(bool criado, int documentoModeloId)> CriarOuAtualizar(CriarDocumentoModeloInputModel documentoModelo, bool atualizaSeExistir)
        {
            var cDocumentoModelo = (await _documentoModeloRepo.Buscar(
                x => x.DocumentoModeloId == documentoModelo.DocumentoModeloId
            )).FirstOrDefault();

            if (cDocumentoModelo == null)
            {
                cDocumentoModelo = DocumentoModelo.CriarParaImportacao(
                    tipoDocumentoId: documentoModelo.TipoDocumentoId,
                    titulo: documentoModelo.Titulo,
                    conteudoTipo: documentoModelo.ConteudoTipo,
                    conteudo: documentoModelo.Conteudo,
                    ativo: documentoModelo.Ativo
                );
                await Salvar(cDocumentoModelo);
                return (true, cDocumentoModelo.DocumentoModeloId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cDocumentoModelo.AtualizarPropriedades(
                    tipoDocumentoId: documentoModelo.TipoDocumentoId,
                    titulo: documentoModelo.Titulo,
                    conteudoTipo: documentoModelo.ConteudoTipo,
                    conteudo: documentoModelo.Conteudo,
                    ativo: documentoModelo.Ativo
                );
                await _documentoModeloRepo.Atualizar(cDocumentoModelo);
                await Atualizar(cDocumentoModelo);
            }

            return (false, documentoModelo.DocumentoModeloId);
        }


        public async Task CriarParaImportacao(int documentoModeloID, int tipoDocumentoId, string titulo, short conteudoTipo, string conteudo, bool? ativo)
        {
            var cDocumentoModelo = (await _documentoModeloRepo.Buscar(
                            x => x.DocumentoModeloId == documentoModeloID)
                            ).FirstOrDefault();
            if (cDocumentoModelo == null)
            {
                cDocumentoModelo = DocumentoModelo.CriarParaImportacao(tipoDocumentoId, titulo, conteudoTipo, conteudo, ativo);
                await Salvar(cDocumentoModelo);
            }
            return;
        }

        public async Task Validar(int documentoModeloID)
        {
            var cDocumentoModelo = (await _documentoModeloRepo.Buscar(x => x.DocumentoModeloId == documentoModeloID)).FirstOrDefault();
            if (cDocumentoModelo == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Modelo de documento com ID {documentoModeloID} não encontrado."
                );
            }
        }
    }
}
