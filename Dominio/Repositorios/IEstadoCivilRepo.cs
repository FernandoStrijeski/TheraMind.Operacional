using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IEstadoCivilRepo : IBaseRepositorio<EstadoCivil>
    {
        Task<List<EstadoCivil>> BuscarFiltros(
        Expression<Func<EstadoCivil, bool>> filtro = null,
        Func<IQueryable<EstadoCivil>, IOrderedQueryable<EstadoCivil>> orderBy = null,
        int skip = 0,
        int take = 0
        );
        Task<EstadoCivil> BuscarPorID(String estadoCivilID);
    }
}
