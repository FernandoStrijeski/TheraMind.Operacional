using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.PacotesFechados;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.PacotesFechados
{
    public class PacoteFechadoRepo : BaseRepositorio<PacoteFechado>, IPacoteFechadoRepo
    {
        public PacoteFechadoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<PacoteFechado>> BuscarFiltros(
            Expression<Func<PacoteFechado, bool>> filtro = null,
            Func<IQueryable<PacoteFechado>, IOrderedQueryable<PacoteFechado>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<PacoteFechado> query = _dbSet.AsNoTracking();

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

        public async Task<PacoteFechado>? BuscarPorID(int pacoteFechadoID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.PacoteFechadoId == pacoteFechadoID);
            return plano;
        }
    }
}
