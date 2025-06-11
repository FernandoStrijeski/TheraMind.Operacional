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
    public class EscolaridadeRepo : BaseRepositorio<Escolaridade, int>, IEscolaridadeRepo
    {
        public EscolaridadeRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Escolaridade>> BuscarFiltros(
            Expression<Func<Escolaridade, bool>> filtro = null,
            Func<IQueryable<Escolaridade>, IOrderedQueryable<Escolaridade>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Escolaridade> query = _dbSet.AsNoTracking();

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

        public async Task<Escolaridade>? BuscarPorID(int escolaridadeID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.EscolaridadeId == escolaridadeID);
            return retorno;
        }
    }
}
