using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Convenios;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Convenios
{
    public class ConvenioRepo : BaseRepositorio<Convenio>, IConvenioRepo
    {
        public ConvenioRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Convenio>> BuscarFiltros(
            Expression<Func<Convenio, bool>> filtro = null,
            Func<IQueryable<Convenio>, IOrderedQueryable<Convenio>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Convenio> query = _dbSet.AsNoTracking();

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

        public async Task<Convenio>? BuscarPorID(int convenioID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.ConvenioId == convenioID);
            return plano;
        }
    }
}
