using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.AcompanhamentosClinicos;
using Dominio.Entidades;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.AcompanhamentosClinicos
{
    public class AcompanhamentoClinicoRepo : BaseRepositorio<AcompanhamentoClinico>, IAcompanhamentoClinicoRepo
    {
        public AcompanhamentoClinicoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<AcompanhamentoClinico>> BuscarFiltros(
            Expression<Func<AcompanhamentoClinico, bool>> filtro = null,
            Func<IQueryable<AcompanhamentoClinico>, IOrderedQueryable<AcompanhamentoClinico>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<AcompanhamentoClinico> query = _dbSet.AsNoTracking();

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

        public async Task<AcompanhamentoClinico>? BuscarPorID(Guid acompanhamentoClinicoID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.AcompanhamentoClinicoId == acompanhamentoClinicoID);
            return plano;
        }
    }
}
