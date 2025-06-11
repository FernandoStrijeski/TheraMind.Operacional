using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using System;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class AuditoriaRepo : BaseRepositorio<Auditoria, int>, IAuditoriaRepo
    {
        public AuditoriaRepo(ApplicationDbContext context) : base(context) { }

        public Task Deletar(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
