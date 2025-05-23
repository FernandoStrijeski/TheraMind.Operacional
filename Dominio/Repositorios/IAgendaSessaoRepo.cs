using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AgendasSessoes
{
    public interface IAgendaSessaoRepo : IBaseRepositorio<AgendaSessao>
    {
        Task<List<AgendaSessao>> BuscarFiltros(
            Expression<Func<AgendaSessao, bool>> filtro = null,
            Func<IQueryable<AgendaSessao>, IOrderedQueryable<AgendaSessao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AgendaSessao>? BuscarPorID(Guid agendaSessaoID);
    }
}
