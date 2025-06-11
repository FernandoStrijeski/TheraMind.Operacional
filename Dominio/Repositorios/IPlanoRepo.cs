using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Repositorios
{
    public interface IPlanoRepo : IBaseRepositorio<Plano, Guid>
    {
        Task<List<Plano>> BuscarFiltros(
            Expression<Func<Plano, bool>> filtro = null,
            Func<IQueryable<Plano>, IOrderedQueryable<Plano>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Plano>? BuscarPorID(Guid planoID);
    }
}
