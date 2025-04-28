using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IIdentidadeGeneroRepo : IBaseRepositorio<IdentidadeGenero>
    {
        Task<List<IdentidadeGenero>> BuscarFiltros(
        Expression<Func<IdentidadeGenero, bool>> filtro = null,
        Func<IQueryable<IdentidadeGenero>, IOrderedQueryable<IdentidadeGenero>> orderBy = null,
        int skip = 0,
        int take = 0
        );
        Task<IdentidadeGenero> BuscarPorID(int identidadeGeneroID);
    }
}
