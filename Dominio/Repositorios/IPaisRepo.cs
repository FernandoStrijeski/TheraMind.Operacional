using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IPaisRepo : IBaseRepositorio<Pais>
    {
        Task<List<Pais>> BuscarFiltros(
        Expression<Func<Pais, bool>> filtro = null,
        Func<IQueryable<Pais>, IOrderedQueryable<Pais>> orderBy = null,
        int skip = 0,
        int take = 0
        );
        Task<Pais> BuscarPorID(int paisID);
    }
}
