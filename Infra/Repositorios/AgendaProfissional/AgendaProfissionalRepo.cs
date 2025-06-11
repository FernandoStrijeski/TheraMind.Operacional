using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.AgendasProfissionais;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.AgendasProfissionais
{
    public class AgendaProfissionalRepo : BaseRepositorio<AgendaProfissional, int>, IAgendaProfissionalRepo
    {
        public AgendaProfissionalRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AgendaProfissional>> BuscarFiltros(
            Expression<Func<AgendaProfissional, bool>> filtro = null,
            Func<IQueryable<AgendaProfissional>, IOrderedQueryable<AgendaProfissional>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AgendaProfissional> query = _dbSet.AsNoTracking();

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

        public async Task<AgendaProfissional>? BuscarPorID(int agendaProfissionalID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.AgendaProfissionalId == agendaProfissionalID);
            return plano;
        }
    }
}
