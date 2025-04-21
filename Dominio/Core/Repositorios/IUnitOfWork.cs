using System.Threading.Tasks;

namespace Dominio.Core.Repositorios
{
    public interface IUnitOfWork
    {
        Task<bool> Comitar();
    }
}
