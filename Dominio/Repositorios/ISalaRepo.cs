using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface ISalaRepo : IBaseRepositorio<Sala>
    {
        Task<List<Sala>> BuscarFiltros(
            Expression<Func<Sala, bool>> filtro = null,
            Func<IQueryable<Sala>, IOrderedQueryable<Sala>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Sala>? BuscarPorID(string salaID);
    }
}
