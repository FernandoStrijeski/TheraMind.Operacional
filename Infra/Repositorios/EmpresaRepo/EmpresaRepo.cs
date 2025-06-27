using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Repositorios
{
    public class EmpresaRepo : BaseRepositorio<Empresa, Guid>, IEmpresaRepo
    {
        public EmpresaRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<Empresa>? BuscarPorID(Guid empresaID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.EmpresaId == empresaID);
            return retorno;
        }

        public async Task<List<Empresa>> BuscarFiltros(
            Expression<Func<Empresa, bool>> filtro = null,
            Func<IQueryable<Empresa>, IOrderedQueryable<Empresa>> orderBy = null,
            int skip = 0,
            int take = 0
            )
        {
            IQueryable<Empresa> query = _dbSet.AsNoTracking();

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
    }
}
