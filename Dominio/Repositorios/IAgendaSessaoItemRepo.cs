using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.AgendasSessaoItens
{
    public interface IAgendaSessaoItemRepo : IBaseRepositorio<AgendaSessaoItem, int>
    {
        Task<List<AgendaSessaoItem>> BuscarFiltros(
            Expression<Func<AgendaSessaoItem, bool>> filtro = null,
            Func<IQueryable<AgendaSessaoItem>, IOrderedQueryable<AgendaSessaoItem>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<AgendaSessaoItem>? BuscarPorID(int agendaSessaoItemID);
    }
}
