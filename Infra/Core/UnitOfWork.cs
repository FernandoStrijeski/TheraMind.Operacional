using System.Threading.Tasks;
using Dominio.Core.Repositorios;
using Infra.Context;

namespace Infra.Core.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _contexto;

        public UnitOfWork(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> Comitar()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}
