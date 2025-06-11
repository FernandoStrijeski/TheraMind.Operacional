using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface ITipoLogradouroRepo : IBaseRepositorio<TipoLogradouro, string>
    {        
        Task<List<TipoLogradouro>> BuscarFiltros(
            Expression<Func<TipoLogradouro, bool>> filtro = null,
            Func<IQueryable<TipoLogradouro>, IOrderedQueryable<TipoLogradouro>> orderBy = null,
            int skip = 0,
            int take = 0
            );

        Task<TipoLogradouro>? BuscarPorID(string tipoLogradouroID);
    }
}
