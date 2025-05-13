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
    public class CidadeRepo : BaseRepositorio<Cidade>, ICidadeRepo
    {
        public CidadeRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Cidade>> BuscarFiltros(
          Expression<Func<Cidade, bool>> filtro = null,
          Func<IQueryable<Cidade>, IOrderedQueryable<Cidade>> orderBy = null,
          int skip = 0,
          int take = 0
          )
        {
            IQueryable<Cidade> query = _dbSet.AsNoTracking();

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
        public async Task<Cidade>? BuscarPorID(int cidadeId)
        {
            var query = _dbSet.AsQueryable();
            var cidade = await query.FirstOrDefaultAsync(where => where.CidadeId == cidadeId);
            return cidade;
        }
        public async Task<Cidade>? BuscarPorIBGE(int codigoIBGE)
        {
            var query = _dbSet.AsQueryable();
            var cidade = await query.FirstOrDefaultAsync(where => where.CodigoIbge == codigoIBGE);
            return cidade;
        }
    }
}
