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
    public class IdentidadeGeneroRepo : BaseRepositorio<IdentidadeGenero, int>, IIdentidadeGeneroRepo
    {
        public IdentidadeGeneroRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<IdentidadeGenero>> BuscarFiltros(
            Expression<Func<IdentidadeGenero, bool>> filtro = null,
            Func<IQueryable<IdentidadeGenero>, IOrderedQueryable<IdentidadeGenero>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<IdentidadeGenero> query = _dbSet.AsNoTracking();

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
        public async Task<IdentidadeGenero>? BuscarPorID(int identidadeGeneroId)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.IdentidadeGeneroId == identidadeGeneroId);
            return retorno;
        }
    }
}
