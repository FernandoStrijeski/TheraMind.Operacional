using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface INacionalidadeRepo : IBaseRepositorio<Nacionalidade>
    {
        Task<List<Nacionalidade>> BuscarFiltros(
            Expression<Func<Nacionalidade, bool>> filtro = null,
            Func<IQueryable<Nacionalidade>, IOrderedQueryable<Nacionalidade>> orderBy = null,
            int skip = 0,
            int take = 0
            );
        Task<Nacionalidade>? BuscarPorID(int nacionalidadeID);
    }
}
