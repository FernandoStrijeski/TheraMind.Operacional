using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Profissionais;
using Dominio.ProfissionaisAcessos;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.ProfissionaisAcessos
{
    public class ProfissionalAcessoServico : ServicoBase, IProfissionalAcessoServico
    {
        private IConfiguration _configuration;
        private IProfissionalAcessoRepo _profissionalAcessoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ProfissionalAcessoServico(
            IConfiguration configuration,
            IProfissionalAcessoRepo profissionalAcessoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _profissionalAcessoRepo = profissionalAcessoRepo;
        }

        public async Task<ProfissionalAcesso>? BuscarPorID(int profissionalAcessoID) => await _profissionalAcessoRepo.BuscarPorID(profissionalAcessoID);

        public async Task<List<ProfissionalAcesso>> BuscarTodos()
        {
            return await _profissionalAcessoRepo.BuscarFiltros();
        }

        public async Task<List<ProfissionalAcesso>> BuscarPorIDProfissional(Guid profissionalID) => await _profissionalAcessoRepo.BuscarPorIDProfissional(profissionalID);

        public async Task Salvar(ProfissionalAcesso profissionalAcesso)
        {
            await _profissionalAcessoRepo.Adicionar(profissionalAcesso);
            await Comitar();
        }

        private async Task Atualizar(ProfissionalAcesso profissionalAcesso)
        {
            await _profissionalAcessoRepo.Atualizar(profissionalAcesso);
            await Comitar();
        }

        public async Task<(bool criado, int profissionalAcessoId)> CriarOuAtualizar(CriarProfissionalAcessoInputModel profissionalAcesso, bool atualizaSeExistir)
        {
            var cProfissionalAcesso = (await _profissionalAcessoRepo.Buscar(
                x => x.ProfissionalId == profissionalAcesso.ProfissionalId
            )).FirstOrDefault();

            if (cProfissionalAcesso == null)
            {
                cProfissionalAcesso = ProfissionalAcesso.CriarParaImportacao(
                    profissionalID: profissionalAcesso.ProfissionalId,                    
                    empresaID: profissionalAcesso.EmpresaId,
                    filialID: profissionalAcesso.FilialId,
                    acessoTipo: profissionalAcesso.AcessoTipo
                );
                await Salvar(cProfissionalAcesso);
                return (true, cProfissionalAcesso.ProfissionalAcessoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cProfissionalAcesso.AtualizarPropriedades(
                    profissionalID: profissionalAcesso.ProfissionalId,
                    empresaID: profissionalAcesso.EmpresaId,
                    filialID: profissionalAcesso.FilialId,
                    acessoTipo: profissionalAcesso.AcessoTipo
                );
                await _profissionalAcessoRepo.Atualizar(cProfissionalAcesso);
                await Atualizar(cProfissionalAcesso);
            }

            return (false, profissionalAcesso.ProfissionalAcessoId);
        }


        public async Task CriarParaImportacao(int profissionalAcessoID, Guid profissionalID, Guid empresaID, int filialID, short acessoTipo)
        {
            var cProfissional = (await _profissionalAcessoRepo.Buscar(
                            x => x.ProfissionalAcessoId == profissionalAcessoID)
                            ).FirstOrDefault();
            if (cProfissional == null)
            {
                cProfissional = ProfissionalAcesso.CriarParaImportacao(profissionalID, empresaID, filialID, acessoTipo);
                await Salvar(cProfissional);
            }
            return;
        }

        public async Task Validar(int profissionalAcessoID)
        {
            var cPlano = (await _profissionalAcessoRepo.Buscar(x => x.ProfissionalAcessoId == profissionalAcessoID)).FirstOrDefault();
            if (cPlano == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Acesso do Profissional com ID {profissionalAcessoID} não encontrado."
                );
            }
        }
    }
}
