using API.modelos;
using API.modelos.InputModels;
using Dominio.Entidades;

namespace API.Servicos.GeradorCNAB240Sicredi
{
    public interface IGeradorCNAB240SicrediServico
    {
        byte[] GerarRemessaCnab240();

        string EnviarRemessaParaSicredi(byte[] arquivoRemessa, string nomeArquivo, out byte[] arquivoRetorno);
    }
}
