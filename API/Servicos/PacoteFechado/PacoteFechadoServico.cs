using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.PacotesFechados;
using Dominio.Profissionais;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.PacotesFechados
{
    public class PacoteFechadoServico : ServicoBase, IPacoteFechadoServico
    {
        private IConfiguration _configuration;
        private IPacoteFechadoRepo _pacoteFechadoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PacoteFechadoServico(
            IConfiguration configuration,
            IPacoteFechadoRepo pacoteFechadoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _pacoteFechadoRepo = pacoteFechadoRepo;
        }

        public async Task<PacoteFechado>? BuscarPorID(int pacoteFechadoID) => await _pacoteFechadoRepo.BuscarPorID(pacoteFechadoID);

        public async Task<List<PacoteFechado>> BuscarTodos()
        {
            return await _pacoteFechadoRepo.BuscarFiltros();
        }

        public async Task Salvar(PacoteFechado pacoteFechado)
        {
            await _pacoteFechadoRepo.Adicionar(pacoteFechado);
            await Comitar();
        }

        private async Task Atualizar(PacoteFechado pacoteFechado)
        {
            await _pacoteFechadoRepo.Atualizar(pacoteFechado);
            await Comitar();
        }

        public async Task<(bool criado, int pacoteFechadoId)> CriarOuAtualizar(CriarPacoteFechadoInputModel pacoteFechado, bool atualizaSeExistir)
        {
            var cPacoteFechado = (await _pacoteFechadoRepo.Buscar(
                x => x.PacoteFechadoId == pacoteFechado.PacoteFechadoId
            )).FirstOrDefault();

            if (cPacoteFechado == null)
            {
                cPacoteFechado = PacoteFechado.CriarParaImportacao(
                    empresaID: pacoteFechado.EmpresaId,
                    filialID: pacoteFechado.FilialId,                    
                    quantidadeSessoes: pacoteFechado.QuantidadeSessoes,
                    valorTotal: pacoteFechado.ValorTotal,
                    ativo: pacoteFechado.Ativo
                );
                await Salvar(cPacoteFechado);
                return (true, cPacoteFechado.PacoteFechadoId); // <-- retorno com o novo ID
            }
            else if (atualizaSeExistir)
            {
                cPacoteFechado.AtualizarPropriedades(
                    empresaID: pacoteFechado.EmpresaId,
                    filialID: pacoteFechado.FilialId,
                    quantidadeSessoes: pacoteFechado.QuantidadeSessoes,
                    valorTotal: pacoteFechado.ValorTotal,
                    ativo: pacoteFechado.Ativo
                );
                await _pacoteFechadoRepo.Atualizar(cPacoteFechado);
                await Atualizar(cPacoteFechado);
            }

            return (false, pacoteFechado.PacoteFechadoId);
        }


        public async Task CriarParaImportacao(int pacoteFechadoID, Guid empresaID, int filialID, int quantidadeSessoes, decimal valorTotal, bool? ativo)
        {
            var cPacoteFechado = (await _pacoteFechadoRepo.Buscar(
                            x => x.PacoteFechadoId == pacoteFechadoID)
                            ).FirstOrDefault();
            if (cPacoteFechado == null)
            {
                cPacoteFechado = PacoteFechado.CriarParaImportacao(empresaID, filialID, quantidadeSessoes, valorTotal, ativo);
                await Salvar(cPacoteFechado);
            }
            return;
        }

        public async Task Validar(int pacoteFechadoID)
        {
            var cPacoteFechado = (await _pacoteFechadoRepo.Buscar(x => x.PacoteFechadoId == pacoteFechadoID)).FirstOrDefault();
            if (cPacoteFechado == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Pacote fechado com ID {pacoteFechadoID} não encontrado."
                );
            }
        }
    }
}
