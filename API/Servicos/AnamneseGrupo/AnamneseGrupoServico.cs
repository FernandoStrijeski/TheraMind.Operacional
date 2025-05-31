using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.AnamneseGrupos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.AnamneseGrupos
{
    public class AnamneseGrupoServico : ServicoBase, IAnamneseGrupoServico
    {
        private IConfiguration _configuration;
        private IAnamneseGrupoRepo _anamneseGrupoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AnamneseGrupoServico(
            IConfiguration configuration,
            IAnamneseGrupoRepo anamneseGrupoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _anamneseGrupoRepo = anamneseGrupoRepo;
        }

        public async Task<AnamneseGrupo>? BuscarPorID(int anamneseSubGrupoID) => await _anamneseGrupoRepo.BuscarPorID(anamneseSubGrupoID);

        public async Task<List<AnamneseGrupo>> BuscarTodos()
        {
            return await _anamneseGrupoRepo.BuscarFiltros();
        }

        public async Task<List<AnamneseGrupo>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _anamneseGrupoRepo.BuscarFiltros(x => x.Titulo.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(AnamneseGrupo anamneseGrupo)
        {
            await _anamneseGrupoRepo.Adicionar(anamneseGrupo);
            await Comitar();
        }

        private async Task Atualizar(AnamneseGrupo anamneseGrupo)
        {
            await _anamneseGrupoRepo.Atualizar(anamneseGrupo);
            await Comitar();
        }

        public async Task<(bool criado, int anamneseGrupoId)> CriarOuAtualizar(CriarAnamneseGrupoInputModel anamneseGrupo, bool atualizaSeExistir)
        {
            var cAnamneseGrupo = (await _anamneseGrupoRepo.Buscar(
                x => x.AnamneseGrupoId == anamneseGrupo.AnamneseGrupoId
            )).FirstOrDefault();

            if (cAnamneseGrupo == null)
            {
                cAnamneseGrupo = AnamneseGrupo.CriarParaImportacao(
                    empresaID: anamneseGrupo.EmpresaId,
                    filialID: anamneseGrupo.FilialId,
                    profissionalID: anamneseGrupo.ProfissionalId,
                    titulo: anamneseGrupo.Titulo,
                    privado: anamneseGrupo.Privado,
                    editadoPorTodos: anamneseGrupo.EditadoPorTodos,
                    ativo: anamneseGrupo.Ativo
                );
                await Salvar(cAnamneseGrupo);
                return (true, cAnamneseGrupo.AnamneseGrupoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cAnamneseGrupo.AtualizarPropriedades(
                    empresaID: anamneseGrupo.EmpresaId,
                    filialID: anamneseGrupo.FilialId,
                    profissionalID: anamneseGrupo.ProfissionalId,
                    titulo: anamneseGrupo.Titulo,
                    privado: anamneseGrupo.Privado,
                    editadoPorTodos: anamneseGrupo.EditadoPorTodos,
                    ativo: anamneseGrupo.Ativo
                );
                await _anamneseGrupoRepo.Atualizar(cAnamneseGrupo);
                await Atualizar(cAnamneseGrupo);
            }

            return (false, anamneseGrupo.AnamneseGrupoId);
        }


        public async Task CriarParaImportacao(int anamneseGrupoID, Guid empresaID, int filialID, Guid profissionalID, string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            var cAnamneseGrupo = (await _anamneseGrupoRepo.Buscar(
                            x => x.AnamneseGrupoId == anamneseGrupoID)
                            ).FirstOrDefault();
            if (cAnamneseGrupo == null)
            {
                cAnamneseGrupo = AnamneseGrupo.CriarParaImportacao(empresaID, filialID, profissionalID, titulo, privado, editadoPorTodos, ativo);
                await Salvar(cAnamneseGrupo);
            }
            return;
        }

        public async Task Validar(int anamneseGrupoID)
        {
            var cModeloAnamneseSg = (await _anamneseGrupoRepo.Buscar(x => x.AnamneseGrupoId == anamneseGrupoID)).FirstOrDefault();
            if (cModeloAnamneseSg == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Grupo de anamnese com ID {anamneseGrupoID} não encontrado."
                );
            }
        }
    }
}
