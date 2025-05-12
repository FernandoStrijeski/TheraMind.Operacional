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
    public class EstadoRepo : BaseRepositorio<Estado>, IEstadoRepo
    {
        public EstadoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Estado>> BuscarFiltros(
          Expression<Func<Estado, bool>> filtro = null,
          Func<IQueryable<Estado>, IOrderedQueryable<Estado>> orderBy = null,
          int skip = 0,
          int take = 0
          )
        {
            IQueryable<Estado> query = _dbSet.AsNoTracking();

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
        public async Task<Estado>? BuscarPorID(string uf)
        {
            var query = _dbSet.AsQueryable();
            var estado = await query.FirstOrDefaultAsync(where => where.Uf == uf);
            return estado;
        }
    }
}
