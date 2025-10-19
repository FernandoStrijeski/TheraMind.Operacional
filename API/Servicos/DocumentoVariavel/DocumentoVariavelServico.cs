using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.DocumentosVariaveis;
using Dominio.Core.Repositorios;
using Dominio.DocumentosVariaveis;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;
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

        public async Task<DocumentoVariavel> Adicionar(DocumentoVariavel documentoVariavel)
        {
            await _documentoVariavelRepo.Adicionar(documentoVariavel);
            await Comitar();
            return documentoVariavel;
        }

        public async Task<DocumentoVariavel> Atualizar(DocumentoVariavel documentoVariavel)
        {
            await _documentoVariavelRepo.Atualizar(documentoVariavel);
            await Comitar();
            return documentoVariavel;
        }

        public async Task Deletar(int escolaridadeID)
        {
            var documentoVariavel = _documentoVariavelRepo.BuscarPorID(escolaridadeID).Result;

            if (documentoVariavel == null)
                throw new HttpErroDeUsuario(HttpStatusCode.NoContent, "Variável do documento não encontrada, verifique o identificador!");

            //escolaridade.MarcarComoDeletado((int)_usuarioContexto.UsuarioId);
            await _documentoVariavelRepo.Deletar(escolaridadeID);
            await Comitar();

            return;
        }
    }
}
