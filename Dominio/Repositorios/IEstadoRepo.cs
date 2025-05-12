using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IEstadoRepo : IBaseRepositorio<Estado>
    {
        Task<List<Estado>> BuscarFiltros(
           Expression<Func<Estado, bool>> filtro = null,
           Func<IQueryable<Estado>, IOrderedQueryable<Estado>> orderBy = null,
           int skip = 0,
           int take = 0
           );
        Task<Estado> BuscarPorID(string uf);
    }
}
