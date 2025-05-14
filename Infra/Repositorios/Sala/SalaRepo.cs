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
    public class SalaRepo : BaseRepositorio<Sala>, ISalaRepo
    {
        public SalaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Sala>> BuscarFiltros(
            Expression<Func<Sala, bool>> filtro = null,
            Func<IQueryable<Sala>, IOrderedQueryable<Sala>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Sala> query = _dbSet.AsNoTracking();

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

        public async Task<Sala>? BuscarPorID(string salaID)
        {
            var query = _dbSet.AsQueryable();
            var sala = await query.FirstOrDefaultAsync(where => where.SalaId == salaID);
            return sala;
        }
    }
}
