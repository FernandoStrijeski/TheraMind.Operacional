using API.Core.Exceptions;
using API.modelos;
using API.modelos.InputModels;
using API.Servicos.Planos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using System.Net;

namespace API.Servicos.Planos
{
    public class PlanoServico : ServicoBase, IPlanoServico
    {
        private IConfiguration _configuration;
        private IPlanoRepo _planoRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public PlanoServico(
            IConfiguration configuration,
            IPlanoRepo planoRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _planoRepo = planoRepo;
        }
        
        public async Task<Plano>? BuscarPorID(Guid planoID) => await _planoRepo.BuscarPorID(planoID);

        public async Task<List<Plano>> BuscarTodos()
        {
            return await _planoRepo.BuscarFiltros();
        }

        public async Task<List<Plano>> BuscarPorNome(BuscarComNomeParametro parametros)
        {
            return await _planoRepo.BuscarFiltros(x => x.NomePlano.ToUpper().Contains(parametros.Nome.ToUpper()));
        }

        public async Task Salvar(Plano plano)
        {
            await _planoRepo.Adicionar(plano);
            await Comitar();
        }

        private async Task Atualizar(Plano plano)
        {
            await _planoRepo.Atualizar(plano);
            await Comitar();
        }

        public async Task<bool> CriarOuAtualizar(CriarPlanoInputModel plano, bool atualizaSeExistir)
        {
            var cPlano = (await _planoRepo.Buscar(
                x => x.PlanoId == plano.PlanoId
            )).FirstOrDefault();
            if (cPlano == null)
            {
                cPlano = Plano.CriarParaImportacao(
                    nomePlano: plano.NomePlano,                
                    valorPlanoMensal: plano.ValorPlanoMensal,
                    valorPlanoAnual: plano.ValorPlanoAnual,
                    descontoPromocional: plano.DescontoPromocional,
                    descontoMeses: plano.DescontoMeses,                
                    ativo: plano.Ativo
                    );
                await Salvar(cPlano);
                return true;
            }
            else if (atualizaSeExistir)
            {
                cPlano.NomePlano = plano.NomePlano;
                cPlano.AtualizarPropriedades(
                    nomePlano: plano.NomePlano,
                    valorPlanoMensal: plano.ValorPlanoMensal,
                    valorPlanoAnual: plano.ValorPlanoAnual,
                    descontoPromocional: plano.DescontoPromocional,
                    descontoMeses: plano.DescontoMeses,
                    ativo: plano.Ativo
                    );
                await _planoRepo.Atualizar(cPlano);
                await Atualizar(cPlano);

            }
            return false;
        }

        public async Task CriarParaImportacao(Guid planoID, string nomePlano, decimal valorPlanoMensal, decimal valorPlanoAnual, decimal descontoPromocional, short descontoMeses, bool ativo)
        {
            var cPlano = (await _planoRepo.Buscar(
                            x => x.PlanoId == planoID)
                            ).FirstOrDefault();
            if (cPlano == null)
            {
                cPlano = Plano.CriarParaImportacao(nomePlano, valorPlanoMensal, valorPlanoAnual, descontoPromocional, descontoMeses, ativo);
                await Salvar(cPlano);
            }
            return;
        }

        public async Task Validar(Guid planoID)
        {
            var cPlano = (await _planoRepo.Buscar(x => x.PlanoId== planoID)).FirstOrDefault();
            if (cPlano == null)
            {
                throw new HttpErroDeUsuario(
                    HttpStatusCode.NotFound,
                    $"Plano com ID {planoID} não encontrado."
                );
            }
        }
    }
}
