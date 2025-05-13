using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;

namespace Infra.Repositorios
{
    public class AuditoriaRepo : BaseRepositorio<Auditoria>, IAuditoriaRepo
    {
        public AuditoriaRepo(ApplicationDbContext context) : base(context) { }

    }
}
