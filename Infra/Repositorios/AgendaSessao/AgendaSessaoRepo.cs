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

namespace Infra.AgendasSessoes
{
    public class AgendaSessaoRepo : BaseRepositorio<AgendaSessao>, IAgendaSessaoRepo
    {
        public AgendaSessaoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AgendaSessao>> BuscarFiltros(
            Expression<Func<AgendaSessao, bool>> filtro = null,
            Func<IQueryable<AgendaSessao>, IOrderedQueryable<AgendaSessao>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AgendaSessao> query = _dbSet.AsNoTracking().Include(agendaSessao => agendaSessao.AgendaSessaoItens)
                                                                  .Include(agendaSessao => agendaSessao.FormularioSessao)
                                                                  .Include(agendaSessao => agendaSessao.Servico)
                                                                  .Include(agendaSessao => agendaSessao.Cliente);

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

        public async Task<AgendaSessao>? BuscarPorID(Guid agendaSessaoID)
        {
            var query = _dbSet.AsQueryable().Include(agendaSessao => agendaSessao.AgendaSessaoItens)
                                            .Include(agendaSessao => agendaSessao.FormularioSessao)
                                            .Include(agendaSessao => agendaSessao.Servico)
                                            .Include(agendaSessao => agendaSessao.Cliente);

            var agendaSessao = await query.FirstOrDefaultAsync(where => where.AgendaSessaoId == agendaSessaoID);
            return agendaSessao;
        }
    }
}
