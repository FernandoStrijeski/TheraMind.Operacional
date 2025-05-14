using System.Threading.Tasks;

namespace Dominio.Service
{
    public interface ITabelaService
    {
        bool TabelaExiste(string nomeDaTabela);
    }
}
