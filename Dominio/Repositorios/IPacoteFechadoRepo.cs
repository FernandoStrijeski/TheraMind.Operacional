using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.PacotesFechados
{
    public interface IPacoteFechadoRepo : IBaseRepositorio<PacoteFechado>
    {
        Task<List<PacoteFechado>> BuscarFiltros(
            Expression<Func<PacoteFechado, bool>> filtro = null,
            Func<IQueryable<PacoteFechado>, IOrderedQueryable<PacoteFechado>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<PacoteFechado>? BuscarPorID(int pacoteFechadoID);
    }
}
