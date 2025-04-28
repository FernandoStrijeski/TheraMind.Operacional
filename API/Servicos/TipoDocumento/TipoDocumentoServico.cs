using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
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

        public async Task Salvar(TipoDocumento tipoDocumento)
        {
            await _tipoDocumentoRepo.Adicionar(tipoDocumento);
            await Comitar();
        }

        private async Task Atualizar(TipoDocumento tipoDocumento)
        {
            await _tipoDocumentoRepo.Atualizar(tipoDocumento);
            await Comitar();
        }

        public async Task<bool> CriarOuAtualizar(CriarTipoDocumentoInputModel tipoDocumento, bool atualizaSeExistir)
        {
            var cTipoDocumento = (await _tipoDocumentoRepo.Buscar(
                x => x.TipoDocumentoId == tipoDocumento.TipoDocumentoID
            )).FirstOrDefault();
            if (cTipoDocumento == null)
            {
                cTipoDocumento = TipoDocumento.CriarParaImportacao(descricao: tipoDocumento.Descricao, ativo: tipoDocumento.Ativo);
                await Salvar(cTipoDocumento);
                return true;
            }
            else if (atualizaSeExistir)
            {
                cTipoDocumento.Descricao = tipoDocumento.Descricao;                
                cTipoDocumento.AtualizarPropriedades(descricao: tipoDocumento.Descricao, ativo: tipoDocumento.Ativo);
                await _tipoDocumentoRepo.Atualizar(cTipoDocumento);
                await Atualizar(cTipoDocumento);

            }
            return false;
        }

        public async Task CriarParaImportacao(int tipoDocumentoID, string descricao, bool ativo)
        {
            var cTipoDocumento = (await _tipoDocumentoRepo.Buscar(
                            x => x.TipoDocumentoId == tipoDocumentoID)
                            ).FirstOrDefault();
            if (cTipoDocumento == null)
            {
                cTipoDocumento = TipoDocumento.CriarParaImportacao(descricao, ativo);
                await Salvar(cTipoDocumento);
            }
            return;
        }

        public async Task Validar(int tipoDocumentoID)
        {
            var cTipoDocumento = (await _tipoDocumentoRepo.Buscar(x => x.TipoDocumentoId== tipoDocumentoID)).FirstOrDefault();
            if (cTipoDocumento == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Tipo de documento com ID {tipoDocumentoID} não encontrada."
                );
            }
        }
    }
}
