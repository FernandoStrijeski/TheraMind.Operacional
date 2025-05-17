using API.modelos.InputModels;
using API.Operacional.Core.Util;
using API.Operacional.modelos.ViewModels;
using Dominio.Core.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.AspNetCore.Mvc;

namespace API.Servicos.Boletos
{
    public class BoletoServico : ServicoBase, IBoletoServico
    {
        private IConfiguration _configuration;
        private IConnectionParamsServico _connectionParamsServico;

        public BoletoServico(
            IConfiguration configuration,
            IConnectionParamsServico connectionParamsServico,
            IUnitOfWork unitOfWork
        ) : base(configuration, unitOfWork)
        {
            _configuration = configuration;
        }
        public BoletoGeradoViewModel GerarBoleto(CriarBoletoInputModel criarBoletoInputModel)
        {
            var generator = new BoletoPdfGenerator();
            return generator.GerarBoletoPdf(criarBoletoInputModel);            
        }
    }
}
