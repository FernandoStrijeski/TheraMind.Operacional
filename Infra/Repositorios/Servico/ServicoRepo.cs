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
    public class ServicoRepo : BaseRepositorio<Servico>, IServicoRepo
    {
        public ServicoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Servico>> BuscarFiltros(
            Expression<Func<Servico, bool>> filtro = null,
            Func<IQueryable<Servico>, IOrderedQueryable<Servico>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Servico> query = _dbSet.AsNoTracking();

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

        public async Task<Servico>? BuscarPorID(int servicoID)
        {
            var query = _dbSet.AsQueryable();
            var servico = await query.FirstOrDefaultAsync(where => where.ServicoId == servicoID);
            return servico;
        }
    }
}
