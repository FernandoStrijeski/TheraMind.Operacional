using API.modelos;
using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using Dominio.Entidades;

namespace API.Servicos.Boletos
{
    public interface IBoletoServico
    {
        /// <summary>
        /// Gera o binário do boleto
        /// </summary>
        /// <returns></returns>
        BoletoGeradoViewModel GerarBoleto(CriarBoletoInputModel criarBoletoInputModel);
    }
}
