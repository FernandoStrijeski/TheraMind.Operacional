using API.modelos.InputModels;
using API.Operacional.Core.Util;
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
        public byte[] GerarBoleto(CriarBoletoInputModel criarBoletoInputModel)
        {
            var generator = new BoletoPdfGenerator();
            var pdfBytes = generator.GerarBoletoPdf(criarBoletoInputModel);
            return pdfBytes;
        }

    }
}
