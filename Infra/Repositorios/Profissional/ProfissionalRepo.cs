using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Profissionais;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Profissionais
{
    public class ProfissionalRepo : BaseRepositorio<Profissional, Guid>, IProfissionalRepo
    {
        public ProfissionalRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Profissional>> BuscarFiltros(
            Expression<Func<Profissional, bool>> filtro = null,
            Func<IQueryable<Profissional>, IOrderedQueryable<Profissional>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Profissional> query = _dbSet.AsNoTracking();

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

        public async Task<Profissional>? BuscarPorID(Guid profissionalID)
        {
            var query = _dbSet.AsQueryable().Include(profissional => profissional.Usuario);
            var plano = await query.FirstOrDefaultAsync(where => where.ProfissionalId == profissionalID);
            return plano;
        }
    }
}
