using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseSubGrupos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AnamneseSubGrupos
{
    public class AnamneseSubGrupoServico : ServicoBase, IAnamneseSubGrupoServico
    {
        private IConfiguration _configuration;
        private IAnamneseSubGrupoRepo _anamneseSubGrupoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseSubGrupoServico(
            IConfiguration configuration,
            IAnamneseSubGrupoRepo anamneseSubGrupoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseSubGrupoRepo = anamneseSubGrupoRepo;
        }

        public async Task<AnamneseSubGrupo>? BuscarPorID(int anamneseSubGrupoID) => await _anamneseSubGrupoRepo.BuscarPorID(anamneseSubGrupoID);

        public async Task<List<AnamneseSubGrupo>> BuscarTodos()
        {
            return await _anamneseSubGrupoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseSubGrupo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseSubGrupoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(AnamneseSubGrupo anamneseSubGrupo)
        {
            await _anamneseSubGrupoRepo.Adicionar(anamneseSubGrupo);
            await Comitar();
        }

        private async Task Atualizar(AnamneseSubGrupo anamneseSubGrupo)
        {
            await _anamneseSubGrupoRepo.Atualizar(anamneseSubGrupo);
            await Comitar();
        }

        public async Task<(bool criado, int anamneseSubGrupoId)> CriarOuAtualizar(CriarAnamneseSubGrupoInputModel anamneseSubGrupo, bool atualizaSeExistir)
        {
            var cAnamneseSubGrupo = (await _anamneseSubGrupoRepo.Buscar(
                x => x.AnamneseSubGrupoId == anamneseSubGrupo.AnamneseSubGrupoId
            )).FirstOrDefault();

            if (cAnamneseSubGrupo == null)
            {
                cAnamneseSubGrupo = AnamneseSubGrupo.CriarParaImportacao(
                    empresaID: anamneseSubGrupo.EmpresaId,
                    filialID: anamneseSubGrupo.FilialId,
                    profissionalID: anamneseSubGrupo.ProfissionalId,
                    anamneseGrupoId: anamneseSubGrupo.AnamneseGrupoId,
                    titulo: anamneseSubGrupo.Titulo,
                    ordem: anamneseSubGrupo.Ordem,
                    ativo: anamneseSubGrupo.Ativo
                );
                await Salvar(cAnamneseSubGrupo);
                return (true, cAnamneseSubGrupo.AnamneseSubGrupoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAnamneseSubGrupo.AtualizarPropriedades(
                    empresaID: anamneseSubGrupo.EmpresaId,
                    filialID: anamneseSubGrupo.FilialId,
                    profissionalID: anamneseSubGrupo.ProfissionalId,
                    anamneseGrupoId: anamneseSubGrupo.AnamneseGrupoId,
                    titulo: anamneseSubGrupo.Titulo,
                    ordem: anamneseSubGrupo.Ordem,
                    ativo: anamneseSubGrupo.Ativo
                );
                await _anamneseSubGrupoRepo.Atualizar(cAnamneseSubGrupo);
                await Atualizar(cAnamneseSubGrupo);
            }

            return (false, anamneseSubGrupo.AnamneseSubGrupoId);
        }


        public async Task CriarParaImportacao(int anamneseGrupoID, Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoId, string titulo, short ordem, bool? ativo)
        {
            var cAnamneseSubGrupo = (await _anamneseSubGrupoRepo.Buscar(
                            x => x.AnamneseGrupoId == anamneseGrupoID)
                            ).FirstOrDefault();
            if (cAnamneseSubGrupo == null)
            {
                cAnamneseSubGrupo = AnamneseSubGrupo.CriarParaImportacao(empresaID, filialID, profissionalID, anamneseGrupoId, titulo, ordem, ativo);
                await Salvar(cAnamneseSubGrupo);
            }
            return;
        }

        public async Task Validar(int anamneseSubGrupoID)
        {
            var cAnamneseSubGrupo = (await _anamneseSubGrupoRepo.Buscar(x => x.AnamneseSubGrupoId == anamneseSubGrupoID)).FirstOrDefault();
            if (cAnamneseSubGrupo == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Subgrupo de anamnese com ID {anamneseSubGrupoID} não encontrado."
                );
            }
        }
    }
}
