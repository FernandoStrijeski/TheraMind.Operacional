using API.modelos;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Configuration;

namespace API.Servicos.Auditorias
{
    public class AuditoriaServico : ServicoBase, IAuditoriaServico
    {
        private IConfiguration _configuration;
        private IAuditoriaRepo _auditoriaRepo;
        private IConnectionParamsServico _connectionParamsServico;

        public AuditoriaServico(
            IConfiguration configuration,
            IAuditoriaRepo auditoriaRepo,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
            _auditoriaRepo = auditoriaRepo;
            _connectionParamsServico = connectionParamsServico;            
        }

        public async Task GravaTrilha(Auditoria auditoria)
        {
            await _auditoriaRepo.Adicionar(auditoria);
            await Comitar();
        }
    }
}
