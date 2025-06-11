using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface ITipoEtniaRepo : IBaseRepositorio<TipoEtnia, int>
    {
        Task<List<TipoEtnia>> BuscarFiltros(
                   Expression<Func<TipoEtnia, bool>> filtro = null,
                   Func<IQueryable<TipoEtnia>, IOrderedQueryable<TipoEtnia>> orderBy = null,
                   int skip = 0,
                   int take = 0
                   );

        Task<TipoEtnia>? BuscarPorID(int tipoLogradouroID);
    }
}
