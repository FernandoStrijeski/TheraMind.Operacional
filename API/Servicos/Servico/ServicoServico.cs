using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Servicos
{
    public class ServicoServico : ServicoBase, IServicoServico
    {
        private IConfiguration _configuration;
        private IServicoRepo _servicoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public ServicoServico(
            IConfiguration configuration,
            IServicoRepo servicoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _servicoRepo = servicoRepo;
        }
        
        public async Task<Servico>? BuscarPorID(int servicoID) => await _servicoRepo.BuscarPorID(servicoID);

        public async Task<List<Servico>> BuscarTodos()
        {
            return await _servicoRepo.BuscarFiltros();
        }

        public async Task<List<Servico>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _servicoRepo.BuscarFiltros(x => x.Nome.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(Servico servico)
        {
            await _servicoRepo.Adicionar(servico);
            await Comitar();
        }

        private async Task Atualizar(Servico servico)
        {
            await _servicoRepo.Atualizar(servico);
            await Comitar();
        }

        public async Task<(bool criado, int servicoId)> CriarOuAtualizar(CriarServicoInputModel servico, bool atualizaSeExistir)
        {
            var cServico = (await _servicoRepo.Buscar(
                x => x.ServicoId == servico.ServicoId
            )).FirstOrDefault();
            if (cServico == null)
            {
                cServico = Servico.CriarParaImportacao(
                    empresaID: servico.EmpresaId,
                    filialID: servico.FilialId,
                    nome: servico.Nome,                
                    padrao: servico.Padrao,
                    duracaoMinutos: servico.DuracaoMinutos,
                    ativo: servico.Ativo
                    );
                await Salvar(cServico);
                return (true, cServico.ServicoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cServico.Nome = servico.Nome;
                cServico.AtualizarPropriedades(
                    empresaID: servico.EmpresaId,
                    filialID: servico.FilialId,
                    nome: servico.Nome,
                    padrao: servico.Padrao,
                    duracaoMinutos: servico.DuracaoMinutos,
                    ativo: servico.Ativo
                    );
                await _servicoRepo.Atualizar(cServico);
                await Atualizar(cServico);

            }
            return (false, cServico.ServicoId); // <-- retorno com o novo ID
        }

        public async Task CriarParaImportacao(int servicoID, Guid empresaID, int filialID, string nome, bool padrao, short? duracaoMinutos, bool? ativo)
        {
            var cServico = (await _servicoRepo.Buscar(
                            x => x.ServicoId == servicoID)
                            ).FirstOrDefault();
            if (cServico == null)
            {
                cServico = Servico.CriarParaImportacao(empresaID, filialID, nome, padrao, duracaoMinutos, ativo);
                await Salvar(cServico);
            }
            return;
        }

        public async Task Validar(int servicoID)
        {
            var cServico = (await _servicoRepo.Buscar(x => x.ServicoId== servicoID)).FirstOrDefault();
            if (cServico == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Serviço com ID {servicoID} não encontrado."
                );
            }
        }
    }
}
