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
    public class GrauParentescoRepo : BaseRepositorio<GrauParentesco, int>, IGrauParentescoRepo
    {
        public GrauParentescoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<GrauParentesco>> BuscarFiltros(
            Expression<Func<GrauParentesco, bool>> filtro = null,
            Func<IQueryable<GrauParentesco>, IOrderedQueryable<GrauParentesco>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<GrauParentesco> query = _dbSet.AsNoTracking();

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
        public async Task<GrauParentesco>? BuscarPorID(int grauParentescoID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.GrauParentescoId == grauParentescoID);
            return retorno;
        }
    }
}
