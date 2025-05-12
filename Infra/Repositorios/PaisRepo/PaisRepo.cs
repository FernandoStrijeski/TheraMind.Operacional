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
    public class PaisRepo : BaseRepositorio<Pais>, IPaisRepo
    {
        public PaisRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Pais>> BuscarFiltros(
          Expression<Func<Pais, bool>> filtro = null,
          Func<IQueryable<Pais>, IOrderedQueryable<Pais>> orderBy = null,
          int skip = 0,
          int take = 0
          )
        {
            IQueryable<Pais> query = _dbSet.AsNoTracking();

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
        public async Task<Pais>? BuscarPorID(int paisID)
        {
            var query = _dbSet.AsQueryable();
            var pais = await query.FirstOrDefaultAsync(where => where.PaisId == paisID);
            return pais;
        }
    }
}
