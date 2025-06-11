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
    public class UsuarioRepo : BaseRepositorio<Usuario, Guid>, IUsuarioRepo
    {
        public UsuarioRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Usuario>> BuscarFiltros(
            Expression<Func<Usuario, bool>> filtro = null,
            Func<IQueryable<Usuario>, IOrderedQueryable<Usuario>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Usuario> query = _dbSet.AsNoTracking();

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

        public async Task<Usuario>? BuscarPorID(Guid usuarioID)
        {
            var query = _dbSet.AsQueryable();
            var plano = await query.FirstOrDefaultAsync(where => where.UsuarioId == usuarioID);
            return plano;
        }
    }
}
