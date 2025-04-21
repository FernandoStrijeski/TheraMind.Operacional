using System.Threading.Tasks;

namespace Infra.Servicos.TheraMindAutenticacao
{
    public interface ITheraMindAutenticacaoServico
    {
        Task<bool> ValidarToken(string token);
    }
}
