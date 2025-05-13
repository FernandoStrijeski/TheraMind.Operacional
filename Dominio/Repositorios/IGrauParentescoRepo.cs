using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IGrauParentescoRepo : IBaseRepositorio<GrauParentesco>
    {
        Task<List<GrauParentesco>> BuscarFiltros(
            Expression<Func<GrauParentesco, bool>> filtro = null,
            Func<IQueryable<GrauParentesco>, IOrderedQueryable<GrauParentesco>> orderBy = null,
            int skip = 0,
            int take = 0
            );
        Task<GrauParentesco>? BuscarPorID(int grauParentescoID);
    }
}
