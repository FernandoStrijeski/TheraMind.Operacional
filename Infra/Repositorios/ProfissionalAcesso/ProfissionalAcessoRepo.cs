using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.ProfissionaisAcessos;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.ProfissionaisAcessos
{
    public class ProfissionalAcessoRepo : BaseRepositorio<ProfissionalAcesso, int>, IProfissionalAcessoRepo
    {
        public ProfissionalAcessoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<ProfissionalAcesso>> BuscarFiltros(
            Expression<Func<ProfissionalAcesso, bool>> filtro = null,
            Func<IQueryable<ProfissionalAcesso>, IOrderedQueryable<ProfissionalAcesso>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<ProfissionalAcesso> query = _dbSet.AsNoTracking();

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

        public async Task<ProfissionalAcesso>? BuscarPorID(int profissionalAcessoID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.ProfissionalAcessoId == profissionalAcessoID);
            return plano;
        }

        public async Task<List<ProfissionalAcesso>> BuscarPorIDProfissional(Guid profissionalID)
        {
            IQueryable<ProfissionalAcesso> query = _dbSet.AsNoTracking();
            query = query.Where(where => where.ProfissionalId == profissionalID);
            return await query.ToListAsync();
        }
    }
}
