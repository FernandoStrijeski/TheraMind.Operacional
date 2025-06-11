using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class PlanoRepo : BaseRepositorio<Plano, Guid>, IPlanoRepo
    {
        public PlanoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Plano>> BuscarFiltros(
            Expression<Func<Plano, bool>> filtro = null,
            Func<IQueryable<Plano>, IOrderedQueryable<Plano>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Plano> query = _dbSet.AsNoTracking();

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

        public async Task<Plano>? BuscarPorID(Guid planoID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.PlanoId == planoID);
            return plano;
        }
    }
}
