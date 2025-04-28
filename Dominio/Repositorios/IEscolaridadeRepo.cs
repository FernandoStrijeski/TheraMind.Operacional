using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IEscolaridadeRepo : IBaseRepositorio<Escolaridade>
    {
        Task<List<Escolaridade>> BuscarFiltros(
        Expression<Func<Escolaridade, bool>> filtro = null,
        Func<IQueryable<Escolaridade>, IOrderedQueryable<Escolaridade>> orderBy = null,
        int skip = 0,
        int take = 0
        );
        Task<Escolaridade>? BuscarPorID(int escolaridadeID);
    }
}
