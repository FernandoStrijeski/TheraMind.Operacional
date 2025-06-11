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
    public class TipoDocumentoRepo : BaseRepositorio<TipoDocumento, int>, ITipoDocumentoRepo
    {
        public TipoDocumentoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<TipoDocumento>> BuscarFiltros(
            Expression<Func<TipoDocumento, bool>> filtro = null,
            Func<IQueryable<TipoDocumento>, IOrderedQueryable<TipoDocumento>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<TipoDocumento> query = _dbSet.AsNoTracking();

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

        public async Task<TipoDocumento>? BuscarPorID(int tipoDocumentoID)
        {
            var query = _dbSet.AsQueryable();
            var tipoDocumento = await query.FirstOrDefaultAsync(where => where.TipoDocumentoId == tipoDocumentoID);
            return tipoDocumento;
        }
    }
}
