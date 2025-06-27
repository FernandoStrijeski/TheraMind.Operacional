using Dominio.Core.Repositorios;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IEmpresaRepo : IBaseRepositorio<Empresa, Guid>
    {
        Task<Empresa>? BuscarPorID(Guid empresaID);

        Task<List<Empresa>> BuscarFiltros(
            Expression<Func<Empresa, bool>> filtro = null,
            Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> orderBy = null,
            int skip = 0,
            int take = 0
            );
    }
}
