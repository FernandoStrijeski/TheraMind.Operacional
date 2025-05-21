using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.DocumentosVariaveis;
using Dominio.Core.Repositorios;
using Dominio.DocumentosVariaveis;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System;
using System.Net;

namespace API.Servicos.DocumentosVariaveis
{
    public class DocumentoVariavelServico : ServicoBase, IDocumentoVariavelServico
    {
        private IConfiguration _configuration;
        private IDocumentoVariavelRepo _documentoVariavelRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public DocumentoVariavelServico(
            IConfiguration configuration,
            IDocumentoVariavelRepo documentoVariavelRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _documentoVariavelRepo = documentoVariavelRepo;
        }

        public async Task<DocumentoVariavel>? BuscarPorID(int documentoVariavelID) => await _documentoVariavelRepo.BuscarPorID(documentoVariavelID);

        public async Task<List<DocumentoVariavel>> BuscarTodos()
        {
            return await _documentoVariavelRepo.BuscarFiltros();
        }

        public async Task<List<DocumentoVariavel>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _documentoVariavelRepo.BuscarFiltros(x => x.NomeVariavel.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(DocumentoVariavel documentoVariavel)
        {
            await _documentoVariavelRepo.Adicionar(documentoVariavel);
            await Comitar();
        }

        private async Task Atualizar(DocumentoVariavel documentoVariavel)
        {
            await _documentoVariavelRepo.Atualizar(documentoVariavel);
            await Comitar();
        }

        public async Task<(bool criado, int documentoVariavelId)> CriarOuAtualizar(CriarDocumentoVariavelInputModel documentoVariavel, bool atualizaSeExistir)
        {
            var cDocumentoVariavel = (await _documentoVariavelRepo.Buscar(
                x => x.DocumentoVariavelId == documentoVariavel.DocumentoVariavelId
            )).FirstOrDefault();

            if (cDocumentoVariavel == null)
            {
                cDocumentoVariavel = DocumentoVariavel.CriarParaImportacao(
                    nomeVariavel: documentoVariavel.NomeVariavel,
                    nomeCampo: documentoVariavel.NomeCampo,
                    nomeTabela: documentoVariavel.NomeTabela,
                    ativo: documentoVariavel.Ativo
                );
                await Salvar(cDocumentoVariavel);
                return (true, cDocumentoVariavel.DocumentoVariavelId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cDocumentoVariavel.AtualizarPropriedades(
                    nomeVariavel: documentoVariavel.NomeVariavel,
                    nomeCampo: documentoVariavel.NomeCampo,
                    nomeTabela: documentoVariavel.NomeTabela,
                    ativo: documentoVariavel.Ativo
                );
                await _documentoVariavelRepo.Atualizar(cDocumentoVariavel);
                await Atualizar(cDocumentoVariavel);
            }

            return (false, documentoVariavel.DocumentoVariavelId);
        }

        public async Task CriarParaImportacao(int documentoVariavelID, string nomeVariavel, string nomeCampo, string nomeTabela, bool? ativo)
        {
            var cConvenio = (await _documentoVariavelRepo.Buscar(
                            x => x.DocumentoVariavelId == documentoVariavelID)
                            ).FirstOrDefault();
            if (cConvenio == null)
            {
                cConvenio = DocumentoVariavel.CriarParaImportacao(nomeVariavel, nomeCampo, nomeTabela, ativo);
                await Salvar(cConvenio);
            }
            return;
        }

        public async Task Validar(int documentoVariavelID)
        {
            var cDocumentoVariavel = (await _documentoVariavelRepo.Buscar(x => x.DocumentoVariavelId == documentoVariavelID)).FirstOrDefault();
            if (cDocumentoVariavel == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Variável de documento com ID {documentoVariavelID} não encontrado."
                );
            }
        }
    }
}
