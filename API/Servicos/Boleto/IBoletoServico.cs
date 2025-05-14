using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.Boletos
{
    public interface IBoletoServico
    {
        /// <summary>
        /// Gera o binário do boleto
        /// </summary>
        /// <returns></returns>
        byte[] GerarBoleto(CriarBoletoInputModel criarBoletoInputModel);
    }
}
