using System.Threading.Tasks;

namespace Dominio.Servico
{
    public interface ITabelaService
    {
        bool TabelaExiste(string nomeDaTabela);
    }
}
