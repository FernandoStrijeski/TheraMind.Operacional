using Dominio.AgendasSessaoItens;
using Dominio.AgendasSessoes;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.AgendasSessaoItens
{
    public class AgendaSessaoItemRepo : BaseRepositorio<AgendaSessaoItem, int>, IAgendaSessaoItemRepo
    {
        public AgendaSessaoItemRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AgendaSessaoItem>> BuscarFiltros(
            Expression<Func<AgendaSessaoItem, bool>> filtro = null,
            Func<IQueryable<AgendaSessaoItem>, IOrderedQueryable<AgendaSessaoItem>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AgendaSessaoItem> query = _dbSet.AsNoTracking().Include(agendaSessaoItem => agendaSessaoItem.FormularioSessaoCampo);

            if (filtro != null)
                query = query.Where(filtro);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<AgendaSessaoItem>? BuscarPorID(int agendaSessaoItemID)
        {
            var query = _dbSet.AsQueryable().Include(agendaSessaoItem => agendaSessaoItem.FormularioSessaoCampo);

            var agendaSessaoItem = await query.FirstOrDefaultAsync(where => where.AgendaSessaoItemId == agendaSessaoItemID);
            return agendaSessaoItem;
        }
    }
}
